using MediatR;

namespace TicketMaster.Core.RequestResponse.Events.Commands.CancelEvent;

public sealed record CancelEventCommand(int EventId) : IRequest;