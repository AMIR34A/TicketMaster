using MediatR;
using TicketMaster.Core.RequestResponse.Events.Commands.CancelEvent;
using TicketMaster.Core.RequestResponse.Events.Commands.CreateEvent;
using TicketMaster.Core.RequestResponse.Events.Commands.FinishEvent;
using TicketMaster.Core.RequestResponse.Events.Commands.PutUpEventForSale;
using TicketMaster.Core.RequestResponse.Events.Queries.GetEvent;

namespace TicketMaster.Endpoint.WebAPI.Endpoints;

public class EventEndpoints(Mediator mediator) : IEndpoint
{
    public void Register(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("event");

        group.MapGet("/", async (GetEventQuery query) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });

        group.MapPost("create", async (CreateEventCommand command) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        group.MapPost("finish", async (FinishEventCommand command) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        });

        group.MapPost("sell", async (SellEventCommand command) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        });

        group.MapPost("cancel", async (CancelEventCommand command) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        });
    }
}