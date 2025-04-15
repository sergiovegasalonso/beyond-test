using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoLists.Application.UseCases.AddItem;

var builder = WebApplication.CreateBuilder(args);

// Ensure the correct environment and configuration file are loaded
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

// Debugging: Print the connection string
Console.WriteLine(builder.Configuration.GetConnectionString("BeyondTestDb"));

// Add services to the container.
builder.AddApplicationServices();
builder.AddInfrastructureServices();

//builder.Services.AddMediatR(typeof(AddItemCommand).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

    var command = new AddItemCommand
    {
        Title = "Sample Itemmmmmmmmmmmmmmgfdf",
        Description = "This is a sample item."
    };

    var x = await mediator.Send(command);
    Console.WriteLine(x);
}

app.Run();
