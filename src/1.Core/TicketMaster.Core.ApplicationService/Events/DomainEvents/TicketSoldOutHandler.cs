using MediatR;
using TicketMaster.Core.ApplicationService.Exceptions;
using TicketMaster.Core.Contracts.Data.Repositories;
using TicketMaster.Core.Domain.Tickets;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.ApplicationService.Events.DomainEvents;

public class TicketSoldOutHandler : IRequestHandler<TicketsSoldOut>
{
    private readonly IEventRepository _eventRepository;

    public TicketSoldOutHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task Handle(TicketsSoldOut request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
            throw new ApplicationServiceException(Error.NotFound(description: "The event with id:{0} was not found.",
                parameters: [request.EventId.ToString()]));

        @event.SellOut(request.ReservedCount);
        await _eventRepository.CommitAsync(cancellationToken);
    }
}