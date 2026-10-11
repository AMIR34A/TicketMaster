using Microsoft.Extensions.Options;
using RedLockNet;
using TicketMaster.Core.ApplicationService.Exceptions;
using TicketMaster.Core.Contracts.Data.Repositories;
using TicketMaster.Shared.Utilities.AppSettings;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.ApplicationService.Services;

public class TicketOperationsService
{
    private readonly IEventRepository _eventRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IDistributedLockFactory _distributedLockFactory;

    private readonly uint _ticketReservationWaiting;
    private const string PrefixLockKey = "ticket:";

    public TicketOperationsService(
        IEventRepository eventRepository,
        ITicketRepository ticketRepository,
        IDistributedLockFactory distributedLockFactory,
        IOptions<AppSettings> options)
    {
        _eventRepository = eventRepository;
        _ticketRepository = ticketRepository;
        _distributedLockFactory = distributedLockFactory;
        _ticketReservationWaiting = options.Value.TicketReservationWaiting;
    }

    public async Task ReserveAsync(int ticketId, Guid reservedBy, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetAsync(ticketId, cancellationToken);

        if (ticket is null)
            throw new ApplicationServiceException(Error.NotFound(description: "Ticket with id: {0} was not found",
                parameters: [ticketId.ToString()]));

        var @event = await _eventRepository.GetAsync(ticket.EventId, cancellationToken);

        if (@event is null)
            throw new ApplicationServiceException(Error.NotFound(description: "The event with id:{0} was not found.",
                parameters: [ticket.EventId.ToString()]));

        if (@event.Status == Domain.Events.EventStatus.SoldOut)
            throw new ApplicationServiceException(Error.NotFound(description: "All tickets for event with id:{0} was already sold out.",
                parameters: [ticket.EventId.ToString()]));

        string lockKey = PrefixLockKey + ticket.Id;

        await using var @lock = await _distributedLockFactory.CreateLockAsync(
            lockKey,
            TimeSpan.FromMinutes(_ticketReservationWaiting));

        if (!@lock.IsAcquired)
            throw new ApplicationServiceException(Error.Failure(description: "The ticket with row: {0} and seat number: {1} was already reserved",
                parameters: [ticket.Row.Value.ToString(), ticket.SeatNumber.Value.ToString()]));

        int reservedTicketsCount = await _ticketRepository.GetReservedTicketsCountAsync(@event.Id, cancellationToken);

        ticket.Reserve(
            reservedBy,
            @event.Capacity,
            reservedTicketsCount,
            _ticketReservationWaiting);
        await _ticketRepository.CommitAsync(cancellationToken);
    }

    public async Task BookAsync(int ticketId, Guid bookedBy, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetAsync(ticketId, cancellationToken);

        if (ticket is null)
            throw new ApplicationServiceException(Error.NotFound(description: "Ticket with id: {0} was not found",
                parameters: [ticketId.ToString()]));

        ticket.Book(bookedBy);
        await _ticketRepository.CommitAsync(cancellationToken);
    }
}