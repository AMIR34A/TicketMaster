using MediatR;
using TicketMaster.Core.ApplicationService.Exceptions;
using TicketMaster.Core.Contracts.Data.Repositories;
using TicketMaster.Core.RequestResponse.Events.Commands.PutUpEventForSale;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.ApplicationService.Events.Commands.SetEventToOnSale;

public class SellEventCommandHandler : IRequestHandler<SellEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public SellEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task Handle(SellEventCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
            throw new ApplicationServiceException(Error.NotFound(description: "The event with id:{0} was not found.",
                parameters: [request.EventId.ToString()]));

        @event.Sell();
        await _eventRepository.CommitAsync(cancellationToken);
    }
}