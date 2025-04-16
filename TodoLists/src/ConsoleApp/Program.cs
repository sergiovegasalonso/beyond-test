using System.Globalization;
using ConsoleApp;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoLists.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.AddInfrastructureServices();

var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

using (var scope = app.Services.CreateScope())
{
    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

    string? userInput;
    do
    {
        userInput = Utils.GetOption();

        switch (userInput)
        {
            case "1":
                await Utils.AddItem(mediator);
                break;

            case "2":
                await Utils.UpdateItem(mediator);
                break;

            case "3":
                await Utils.RemoveItem(mediator);
                break;

            case "4":
                await Utils.RegisterProgression(mediator);
                break;

            case "5":
                await Utils.PrintItems(mediator);
                break;

            case "0":
                Console.WriteLine("Exiting...");
                break;

            default:
                Console.WriteLine("Invalid option. Please try again.");
                Console.Clear();
                break;
        }
    } while (userInput != "0");
}

app.Run();

