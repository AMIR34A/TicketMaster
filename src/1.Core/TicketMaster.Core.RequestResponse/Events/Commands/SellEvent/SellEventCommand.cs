using MediatR;

namespace TicketMaster.Core.RequestResponse.Events.Commands.PutUpEventForSale;

public sealed record SellEventCommand(int EventId) : IRequest;