using MediatR;
using TicketMaster.Core.Contracts.Data.Repositories;
using TicketMaster.Core.Domain.Events;
using TicketMaster.Core.RequestResponse.Events.Commands.CreateEvent;

namespace TicketMaster.Core.ApplicationService.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, CreateEventCommandResponse>
{
    private readonly IEventRepository _eventRepository;

    public CreateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<CreateEventCommandResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var @event = Event.Create(request.Title,
            request.Type,
            request.DateTimeUtc,
            request.DurationMinute,
            request.Capacity,
            request.OwnerId);

        _eventRepository.Insert(@event);
        await _eventRepository.CommitAsync(cancellationToken);

        return new(@event.Id);
    }
}