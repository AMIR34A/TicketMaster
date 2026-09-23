using MediatR;

namespace TicketMaster.Core.RequestResponse.Events.Queries.GetEvent;

public sealed record GetEventQuery(int EventId) : IRequest<GetEventQueryResponse>;
