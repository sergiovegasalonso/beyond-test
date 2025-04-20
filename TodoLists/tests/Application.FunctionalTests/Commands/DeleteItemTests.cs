using TodoLists.Application.Common.Exceptions;
using TodoLists.Application.UseCases.AddItem;
using TodoLists.Application.UseCases.RegisterProgression;
using TodoLists.Application.UseCases.RemoveItem;
using TodoLists.Domain.Entities;

namespace TodoLists.Application.FunctionalTests;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Testing;

public class DeleteItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new RemoveItemCommand() { Id = 99 };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteItem()
    {
        var itemId = await SendAsync(new AddItemCommand
        {
            Title = "Title",
            Description = "Description",
            Category = "Category",
        });

        await SendAsync(new RegisterProgressionCommand
        {
            TodoItemId = itemId,
            Date = DateTime.UtcNow,
            Percent = 15
        });

        await SendAsync(new RemoveItemCommand() { Id = itemId });

        var item = await FindAsync<TodoItem>(itemId);
        item.Should().BeNull();
    }

    [Test]
    public async Task ShouldRequireThatItemDoesNotHaveAProgressionWithMoreThan50PercentCompleted()
    {
        var itemId = await SendAsync(new AddItemCommand
        {
            Title = "Title",
            Description = "Description",
            Category = "Category",
        });

        await SendAsync(new RegisterProgressionCommand
        {
            TodoItemId = itemId,
            Date = DateTime.UtcNow,
            Percent = 51
        });

        var command = new RemoveItemCommand() { Id = itemId };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
}
