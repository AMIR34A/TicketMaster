namespace TicketMaster.UnitTests.Domain;

using FluentAssertions;
using TicketMaster.Core.Domain.Exceptions;
using TicketMaster.Core.Domain.Tickets;

public class TicketTests
{
    private readonly int _eventId = 1;
    private readonly int _row = 5;
    private readonly int _seatNumber = 10;
    private readonly Guid _userId = Guid.NewGuid();

    [Fact]
    public void Create_ReturnsAvailableTicket_WhenCalledWithValidParameters()
    {
        // Act
        var ticket = Ticket.Create(_eventId, _row, _seatNumber);

        // Assert
        ticket.Should().NotBeNull();
        ticket.EventId.Should().Be(_eventId);
        ticket.Row.Should().Be(_row);
        ticket.SeatNumber.Should().Be(_seatNumber);
        ticket.Status.Should().Be(TicketStatus.Available);
        ticket.BookedOrReservedBy.Should().BeNull();
        ticket.BookedOrReservedAtUtc.Should().BeNull();
    }

    [Fact]
    public void Reserve_ChangesStatusToReserved_WhenCalledOnAvailableTicket()
    {
        // Arrange
        var ticket = Ticket.Create(_eventId, _row, _seatNumber);
        var reservedBy = Guid.NewGuid();
        var beforeReserve = DateTime.UtcNow;

        // Act
        ticket.Reserve(reservedBy);
        var afterReserve = DateTime.UtcNow;

        // Assert
        ticket.Status.Should().Be(TicketStatus.Reserved);
        ticket.BookedOrReservedBy.Should().Be(reservedBy);
        ticket.BookedOrReservedAtUtc.Should().BeOnOrAfter(beforeReserve);
        ticket.BookedOrReservedAtUtc.Should().BeOnOrBefore(afterReserve);
    }

    [Fact]
    public void Reserve_ThrowsDomainException_WhenCalledOnAlreadyReservedTicket()
    {
        // Arrange
        var ticket = Ticket.Create(_eventId, _row, _seatNumber);
        ticket.Reserve(_userId);

        // Act & Assert
        var action = () => ticket.Reserve(Guid.NewGuid());
        action.Should()
            .Throw<DomainException>()
            .And.Message.Should().Be($"Ticket with row: {_row} and seat number: {_seatNumber} was already {TicketStatus.Reserved}.");
    }

    [Fact]
    public void Reserve_ThrowsDomainException_WhenCalledOnAlreadyBookedTicket()
    {
        // Arrange
        var ticket = Ticket.Create(_eventId, _row, _seatNumber);
        ticket.Book(_userId);

        // Act & Assert
        var action = () => ticket.Reserve(Guid.NewGuid());
        action.Should()
            .Throw<DomainException>()
            .And.Message.Should().Be($"Ticket with row: {_row} and seat number: {_seatNumber} was already {TicketStatus.Booked}.");
    }

    [Fact]
    public void Book_ChangesStatusToBooked_WhenCalledOnAvailableTicket()
    {
        // Arrange
        var ticket = Ticket.Create(_eventId, _row, _seatNumber);
        var bookedBy = Guid.NewGuid();
        var beforeBook = DateTime.UtcNow;

        // Act
        ticket.Book(bookedBy);
        var afterBook = DateTime.UtcNow;

        // Assert
        ticket.Status.Should().Be(TicketStatus.Booked);
        ticket.BookedOrReservedBy.Should().Be(bookedBy);
        ticket.BookedOrReservedAtUtc.Should().BeOnOrAfter(beforeBook);
        ticket.BookedOrReservedAtUtc.Should().BeOnOrBefore(afterBook);
    }

    [Fact]
    public void Book_ChangesStatusToBooked_WhenCalledOnReservedTicket()
    {
        // Arrange
        var ticket = Ticket.Create(_eventId, _row, _seatNumber);
        var reservedBy = Guid.NewGuid();
        var bookedBy = Guid.NewGuid();
        ticket.Reserve(reservedBy);

        // Act
        ticket.Book(bookedBy);

        // Assert
        ticket.Status.Should().Be(TicketStatus.Booked);
        ticket.BookedOrReservedBy.Should().Be(bookedBy);
    }

    [Fact]
    public void Book_ThrowsDomainException_WhenCalledOnAlreadyBookedTicket()
    {
        // Arrange
        var ticket = Ticket.Create(_eventId, _row, _seatNumber);
        ticket.Book(_userId);

        // Act & Assert
        var action = () => ticket.Book(Guid.NewGuid());
        action.Should()
            .Throw<DomainException>()
            .And.Message.Should().Be($"Ticket with row: {_row} and seat number: {_seatNumber} was already booked.");
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(5, 10, 15)]
    [InlineData(100, 50, 99)]
    public void Create_CreatesTicketWithCorrectProperties_WhenCalledWithVariousRowsAndSeats(int eventId, int row, int seat)
    {
        // Act
        var ticket = Ticket.Create(eventId, row, seat);

        // Assert
        ticket.EventId.Should().Be(eventId);
        ticket.Row.Should().Be(row);
        ticket.SeatNumber.Should().Be(seat);
    }

    [Fact]
    public void Reserve_OnlyFirstUserSucceeds_WhenMultipleUsersReserveSameTicket()
    {
        // Arrange
        var ticket = Ticket.Create(_eventId, _row, _seatNumber);
        var firstUser = Guid.NewGuid();
        var secondUser = Guid.NewGuid();

        // Act
        ticket.Reserve(firstUser);

        // Assert
        ticket.BookedOrReservedBy.Should().Be(firstUser);
        var action = () => ticket.Reserve(secondUser);
        action.Should()
            .Throw<DomainException>()
            .And.Message.Should().Be($"Ticket with row: {_row} and seat number: {_seatNumber} was already {TicketStatus.Reserved}.");
    }

    [Fact]
    public void Book_UpdatesBookedByAfterReserve_WhenCalledAfterReservation()
    {
        // Arrange
        var ticket = Ticket.Create(_eventId, _row, _seatNumber);
        var reservedBy = Guid.NewGuid();
        var bookedBy = Guid.NewGuid();
        var reserveTime = DateTime.UtcNow;
        ticket.Reserve(reservedBy);

        // Act
        ticket.Book(bookedBy);

        // Assert
        ticket.Status.Should().Be(TicketStatus.Booked);
        ticket.BookedOrReservedBy.Should().Be(bookedBy);
        ticket.BookedOrReservedAtUtc.Should().BeAfter(reserveTime);
    }

    [Fact]
    public void Reserve_UpdatesUserForEachReservation_WhenMultipleDifferentTicketsAreReserved()
    {
        // Arrange
        var ticket1 = Ticket.Create(_eventId, _row, 1);
        var ticket2 = Ticket.Create(_eventId, _row, 2);
        var user1 = Guid.NewGuid();
        var user2 = Guid.NewGuid();

        // Act
        ticket1.Reserve(user1);
        ticket2.Reserve(user2);

        // Assert
        ticket1.BookedOrReservedBy.Should().Be(user1);
        ticket2.BookedOrReservedBy.Should().Be(user2);
        ticket1.BookedOrReservedBy!.Value.Should().NotBe(ticket2.BookedOrReservedBy!.Value);
    }
}