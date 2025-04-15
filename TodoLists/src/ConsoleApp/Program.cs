using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TodoLists.Application.UseCases.AddItem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddApplicationServices();
builder.AddInfrastructureServices();

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
