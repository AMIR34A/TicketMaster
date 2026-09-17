using TicketMaster.Endpoint.WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplication(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();