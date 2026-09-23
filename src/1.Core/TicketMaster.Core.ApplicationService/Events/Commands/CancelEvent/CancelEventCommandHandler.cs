using MediatR;
using TicketMaster.Core.ApplicationService.Exceptions;
using TicketMaster.Core.Contracts.Data.Repositories;
using TicketMaster.Core.RequestResponse.Events.Commands.CancelEvent;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.ApplicationService.Events.Commands.SetEventToCancelled;

public class CancelEventCommandHandler : IRequestHandler<CancelEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public CancelEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task Handle(CancelEventCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
            throw new ApplicationServiceException(Error.NotFound(description: "The event with id:{0} was not found.",
                parameters: [request.EventId.ToString()]));

        @event.Cancel();
        await _eventRepository.CommitAsync(cancellationToken);
    }
}