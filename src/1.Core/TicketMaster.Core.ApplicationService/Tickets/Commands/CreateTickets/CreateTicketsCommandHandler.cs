using MediatR;
using TicketMaster.Core.ApplicationService.Exceptions;
using TicketMaster.Core.Contracts.Data.Repositories;
using TicketMaster.Core.Domain.Tickets;
using TicketMaster.Core.RequestResponse.Tickets.Commands.CreateTickets;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.ApplicationService.Tickets.Commands.CreateTickets;

public class CreateTicketsCommandHandler : IRequestHandler<CreateTicketsCommand, CreateTicketsCommandResponse>
{
    private readonly IEventRepository _eventRepository;
    private readonly ITicketRepository _ticketRepository;

    public CreateTicketsCommandHandler(IEventRepository eventRepository, ITicketRepository ticketRepository)
    {
        _eventRepository = eventRepository;
        _ticketRepository = ticketRepository;
    }

    public async Task<CreateTicketsCommandResponse> Handle(CreateTicketsCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
            throw new ApplicationServiceException(Error.NotFound(description: "The event with id:{0} was not found.",
                parameters: [request.EventId.ToString()]));

        var existedTicketsCount = await _ticketRepository.GetTicketsCountByEventIdAsync(@event.Id, cancellationToken);
        int remainingCapacity = @event.Capacity.Value - existedTicketsCount;

        if (remainingCapacity <= 0)
            throw new ApplicationServiceException(Error.Failure(description: "All tickets for event with id:{0} already was created.",
                parameters: [request.EventId.ToString()]));

        var tickets = request.TicketsInfo
            .Take(remainingCapacity)
            .Select(ticketInfo => Ticket.Create(request.EventId,
            ticketInfo.Row,
            ticketInfo.SeatNumber));

        _ticketRepository.InsertRange(tickets);
        await _ticketRepository.CommitAsync(cancellationToken);

        return new(remainingCapacity,
            remainingCapacity + existedTicketsCount,
            @event.Capacity.Value,
            tickets.Select(t => new TicketInfo(t.Row, t.SeatNumber)));
    }
}