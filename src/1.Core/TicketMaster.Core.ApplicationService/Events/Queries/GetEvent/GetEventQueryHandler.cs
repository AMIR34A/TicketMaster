using MediatR;
using TicketMaster.Core.ApplicationService.Exceptions;
using TicketMaster.Core.Contracts.Data.Repositories;
using TicketMaster.Core.RequestResponse.Events.Queries.GetEvent;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.ApplicationService.Events.Queries.GetEvent;

public class GetEventQueryHandler : IRequestHandler<GetEventQuery, GetEventQueryResponse>
{
    private readonly IEventRepository _eventRepository;

    public GetEventQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }


    public async Task<GetEventQueryResponse> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
            throw new ApplicationServiceException(Error.NotFound(description: "The event with id:{0} was not found.",
                parameters: [request.EventId.ToString()]));

        return new(@event.Title,
            @event.Type,
            @event.Status,
            @event.DateTimeUtc,
            @event.DurationMinute.Value,
            @event.Capacity.Value,
            @event.OwnerId);
    }
}