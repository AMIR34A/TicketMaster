namespace TicketMaster.UnitTests.Domain;

using FluentAssertions;
using TicketMaster.Core.Domain.Events;

public class EventTests
{
    private readonly Guid _ownerId = Guid.NewGuid();
    private readonly string _title = "Concert 2025";
    private readonly EventType _type = EventType.Music;
    private readonly EventStatus _status = EventStatus.Draft;
    private readonly Duration _duration = new(120);
    private readonly Capacity _capacity = new(1000);

    [Fact]
    public void Create_ReturnsEventWithAllProperties_WhenCalledWithValidParameters()
    {
        // Act
        var createdEvent = Event.Create(_title, _type, _status, _duration, _capacity, _ownerId);

        // Assert
        createdEvent.Should().NotBeNull();
        createdEvent.Title.Should().Be(_title);
        createdEvent.Type.Should().Be(_type);
        createdEvent.Status.Should().Be(_status);
        createdEvent.DurationMinute.Should().Be(_duration);
        createdEvent.Capacity.Should().Be(_capacity);
        createdEvent.OwnerId.Should().Be(_ownerId);
    }

    [Fact]
    public void Create_SetsDateTimeUtc_WhenCalledWithValidParameters()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var createdEvent = Event.Create(_title, _type, _status, _duration, _capacity, _ownerId);
        var afterCreation = DateTime.UtcNow;

        // Assert
        createdEvent.DateTimeUtc.Should().BeOnOrAfter(beforeCreation);
        createdEvent.DateTimeUtc.Should().BeOnOrBefore(afterCreation);
    }

    [Fact]
    public void IsAvailable_ReturnsTrue_WhenEventStatusIsOnSale()
    {
        // Arrange
        var onSaleEvent = Event.Create(_title, _type, EventStatus.OnSale, _duration, _capacity, _ownerId);

        // Act
        var isAvailable = onSaleEvent.IsAvailable();

        // Assert
        isAvailable.Should().BeTrue();
    }

    [Theory]
    [InlineData(EventStatus.Draft)]
    [InlineData(EventStatus.SoldOut)]
    [InlineData(EventStatus.Cancelled)]
    [InlineData(EventStatus.Finished)]
    public void IsAvailable_ReturnsFalse_WhenEventStatusIsNotOnSale(EventStatus status)
    {
        // Arrange
        var unavailableEvent = Event.Create(_title, _type, status, _duration, _capacity, _ownerId);

        // Act
        var isAvailable = unavailableEvent.IsAvailable();

        // Assert
        isAvailable.Should().BeFalse();
    }

    [Fact]
    public void Create_StoresCorrectCapacity_WhenCalledWithDifferentCapacities()
    {
        // Arrange
        var smallCapacity = new Capacity(100);
        var largeCapacity = new Capacity(10000);

        // Act
        var smallEvent = Event.Create(_title, _type, _status, _duration, smallCapacity, _ownerId);
        var largeEvent = Event.Create(_title, _type, _status, _duration, largeCapacity, _ownerId);

        // Assert
        smallEvent.Capacity.Should().Be(smallCapacity);
        largeEvent.Capacity.Should().Be(largeCapacity);
    }

    [Fact]
    public void Create_StoresCorrectDuration_WhenCalledWithDifferentDurations()
    {
        // Arrange
        var shortDuration = new Duration(30);
        var longDuration = new Duration(240);

        // Act
        var shortEvent = Event.Create(_title, _type, _status, shortDuration, _capacity, _ownerId);
        var longEvent = Event.Create(_title, _type, _status, longDuration, _capacity, _ownerId);

        // Assert
        shortEvent.DurationMinute.Should().Be(shortDuration);
        longEvent.DurationMinute.Should().Be(longDuration);
    }

    [Theory]
    [InlineData(EventType.Music)]
    [InlineData(EventType.Sport)]
    [InlineData(EventType.Theatre)]
    public void Create_StoresCorrectType_WhenCalledWithDifferentEventTypes(EventType eventType)
    {
        // Act
        var createdEvent = Event.Create(_title, eventType, _status, _duration, _capacity, _ownerId);

        // Assert
        createdEvent.Type.Should().Be(eventType);
    }

    [Fact]
    public void Create_StoresCorrectOwnerId_WhenCalledWithDifferentOwnerIds()
    {
        // Arrange
        var owner1 = Guid.NewGuid();
        var owner2 = Guid.NewGuid();

        // Act
        var event1 = Event.Create(_title, _type, _status, _duration, _capacity, owner1);
        var event2 = Event.Create(_title, _type, _status, _duration, _capacity, owner2);

        // Assert
        event1.OwnerId.Should().Be(owner1);
        event2.OwnerId.Should().Be(owner2);
        event1.OwnerId.Should().NotBe(event2.OwnerId);
    }

    [Fact]
    public void Create_CreatesEventWithEmptyTitle_WhenCalledWithEmptyTitle()
    {
        // Arrange
        var emptyTitle = string.Empty;

        // Act
        var createdEvent = Event.Create(emptyTitle, _type, _status, _duration, _capacity, _ownerId);

        // Assert
        createdEvent.Title.Should().Be(emptyTitle);
    }
}