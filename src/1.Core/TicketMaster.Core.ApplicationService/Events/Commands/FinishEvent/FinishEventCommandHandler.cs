using MediatR;
using TicketMaster.Core.ApplicationService.Exceptions;
using TicketMaster.Core.Contracts.Data.Repositories;
using TicketMaster.Core.RequestResponse.Events.Commands.FinishEvent;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.ApplicationService.Events.Commands.SetEventToFinished;

public class FinishEventCommandHandler : IRequestHandler<FinishEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public FinishEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task Handle(FinishEventCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
            throw new ApplicationServiceException(Error.NotFound(description: "The event with id:{0} was not found.",
                parameters: [request.EventId.ToString()]));

        @event.Finish();
        await _eventRepository.CommitAsync(cancellationToken);
    }
}