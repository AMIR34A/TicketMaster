using MediatR;
using TicketMaster.Core.RequestResponse.Tickets.Commands.BookTicket;
using TicketMaster.Core.RequestResponse.Tickets.Commands.CreateTickets;
using TicketMaster.Core.RequestResponse.Tickets.Commands.ReserveTicket;
using TicketMaster.Core.RequestResponse.Tickets.Queries.GetTickets;

namespace TicketMaster.Endpoint.WebAPI.Endpoints;

public class TicketEndpoints(IMediator mediator)
{
    public void Register(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("ticket");

        group.MapGet("/", async (GetTicketsQuery query) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });

        group.MapPost("create", async (CreateTicketsCommand command) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        group.MapPost("reserve", async (ReserveTicketCommand command) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        });

        group.MapPost("book", async (BookTicketCommand command) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        });
    }
}