using TodoLists.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TodoLists.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.TodoItems.Any() || !_context.Progressions.Any())
        {
            var firstTodoItem = _context.TodoItems.Add(new TodoItem
            {
                Title = "First todo item :)",
                Description = "Description",
                Category = "My tasks"
            });

            var secondTodoItem = _context.TodoItems.Add(new TodoItem
            {
                Title = "Second todo item :)",
                Description = "Description",
                Category = "My tasks"
            });

            var thirdTodoItem = _context.TodoItems.Add(new TodoItem
            {
                Title = "Third todo item :)",
                Description = "Description",
                Category = "My dreams"
            });

            var forthTodoItem = _context.TodoItems.Add(new TodoItem
            {
                Title = "Fourth todo item :)",
                Description = "Description",
                Category = "My dreams"
            });

            await _context.SaveChangesAsync();

            _context.Progressions.Add(new Progression { TodoItemId = firstTodoItem.Entity.Id, Date = DateTime.UtcNow, Percent = 0 });
            _context.Progressions.Add(new Progression { TodoItemId = firstTodoItem.Entity.Id, Date = DateTime.UtcNow, Percent = 1 });
            _context.Progressions.Add(new Progression { TodoItemId = firstTodoItem.Entity.Id, Date = DateTime.UtcNow, Percent = 75 });
            _context.Progressions.Add(new Progression { TodoItemId = firstTodoItem.Entity.Id, Date = DateTime.UtcNow, Percent = 100 });
            _context.Progressions.Add(new Progression { TodoItemId = secondTodoItem.Entity.Id, Date = DateTime.UtcNow, Percent = 0 });
            _context.Progressions.Add(new Progression { TodoItemId = thirdTodoItem.Entity.Id, Date = DateTime.UtcNow, Percent = 0 });
            _context.Progressions.Add(new Progression { TodoItemId = thirdTodoItem.Entity.Id, Date = DateTime.UtcNow, Percent = 84 });
            _context.Progressions.Add(new Progression { TodoItemId = forthTodoItem.Entity.Id, Date = DateTime.UtcNow, Percent = 0 });

            await _context.SaveChangesAsync();
        }
    }
}
