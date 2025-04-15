using TodoLists.Application.UseCases.AddItem;
using TodoLists.Application.UseCases.RemoveItem;
using TodoLists.Domain.Entities;

namespace TodoLists.Application.FunctionalTests.TodoItems.Commands;

using static Testing;

public class DeleteTodoItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new RemoveItemCommand() { Id = 99 };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoItem()
    {
        var itemId = await SendAsync(new AddItemCommand
        {
            Title = "New Item"
        });

        await SendAsync(new RemoveItemCommand() { Id = itemId });

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().BeNull();
    }
}
