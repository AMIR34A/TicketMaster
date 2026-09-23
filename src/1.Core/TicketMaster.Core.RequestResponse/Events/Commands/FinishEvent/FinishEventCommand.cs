using MediatR;

namespace TicketMaster.Core.RequestResponse.Events.Commands.FinishEvent;

public sealed record FinishEventCommand(int EventId) : IRequest;