using MediatR;
using TodoLists.Application.UseCases.AddItem;
using TodoLists.Application.UseCases.GetItems;
using TodoLists.Application.UseCases.RegisterProgression;
using TodoLists.Application.UseCases.RemoveItem;
using TodoLists.Application.UseCases.UpdateItem;

namespace ConsoleApp;

public static class Utils
{
    public static string? GetOption()
    {
        Console.Clear();
        Console.WriteLine($"Select an option:");
        Console.WriteLine($"1) Add Item");
        Console.WriteLine($"2) Update Item");
        Console.WriteLine($"3) Remove Item");
        Console.WriteLine($"4) Register Progression");
        Console.WriteLine($"5) Print Items");
        Console.WriteLine($"0) Exit");
        var userInput = Console.ReadLine();
        Console.Clear();

        return userInput;
    }

    public static async Task AddItem(IMediator mediator)
    {
        Console.WriteLine($"Adding Item");
        Console.WriteLine($"===========");
        string? id;
        do
        {
            Console.Write($"Id: ");
            id = Console.ReadLine();
        } while (!int.TryParse(id, out _));
        Console.Write($"Title: ");
        var title = Console.ReadLine();
        Console.Write($"Description: ");
        var description = Console.ReadLine();
        Console.Write($"Category: ");
        var category = Console.ReadLine();

        try
        {
            var result = await mediator.Send(new AddItemCommand
            {
                Id = int.Parse(id),
                Title = title,
                Description = description,
                Category = category
            });
            Console.WriteLine($"Item added: {result}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.Write($"Press enter to continue...");
        Console.ReadLine();
    }

    public static async Task UpdateItem(IMediator mediator)
    {
        Console.WriteLine($"Updating Item");
        Console.WriteLine($"=============");
        string? id;
        do
        {
            Console.Write($"Id: ");
            id = Console.ReadLine();
        } while (!int.TryParse(id, out _));
        Console.Write($"Description: ");
        var description = Console.ReadLine();

        try
        {
            await mediator.Send(new UpdateItemCommand
            {
                Id = int.Parse(id),
                Description = description
            });

            Console.WriteLine($"Item updated");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.Write($"Press enter to continue...");
        Console.ReadLine();
    }

    public static async Task RemoveItem(IMediator mediator)
    {
        Console.WriteLine($"Removing Item");
        Console.WriteLine($"=============");
        string? id;
        do
        {
            Console.Write($"Id: ");
            id = Console.ReadLine();
        } while (!int.TryParse(id, out _));

        try
        {
            await mediator.Send(new RemoveItemCommand
            {
                Id = int.Parse(id)
            });

            Console.WriteLine($"Item removed");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.Write($"Press enter to continue...");
        Console.ReadLine();
    }

    public static async Task RegisterProgression(IMediator mediator)
    {
        Console.WriteLine($"Registering Progression");
        Console.WriteLine($"======================");
        string? todoItemId;
        do
        {
            Console.Write($"Todo Item Id: ");
            todoItemId = Console.ReadLine();
        } while (!int.TryParse(todoItemId, out _));

        decimal percent;
        string? percentInput;
        do
        {
            Console.Write($"Percent (e.g., 50.5): ");
            percentInput = Console.ReadLine();
        } while (!decimal.TryParse(percentInput, out percent));

        try
        {
            await mediator.Send(new RegisterProgressionCommand
            {
                TodoItemId = int.Parse(todoItemId),
                Percent = percent
            });

            Console.WriteLine($"Progression registered");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.Write($"Press enter to continue...");
        Console.ReadLine();
    }

    public static async Task PrintItems(IMediator mediator)
    {
        Console.WriteLine($"Print Progression");
        Console.WriteLine($"======================");

        try
        {
            var result = await mediator.Send(new GetItemsQuery());
            PrintItemsWithTableFormat(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.Write($"Press enter to continue...");
        Console.ReadLine();
    }

    private static void PrintItemsWithTableFormat(IEnumerable<TodoItemDto>? list)
    {
        if (list == null) return;

        foreach (var item in list)
        {
            Console.WriteLine($"{item.Id}) {item.Title} - {item.Description} ({item.Category}) Completed:{item.IsCompleted}.");
            foreach (var progression in item.Progressions)
            {
                int barLength = 100;
                var filledLength = (int)(progression.Percent / 100 * barLength);
                var progressBar = new string('O', filledLength).PadRight(barLength, ' ');
                var percentFormatted = $"{progression.Percent.ToString("F0")}%";

                var progressionString = $"{progression.Date} - {percentFormatted, -5}" + $"| {progressBar, -100} |";

                Console.WriteLine(progressionString);
            }
        }
    }
}
