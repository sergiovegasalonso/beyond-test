using TodoLists.Application.UseCases.AddItem;
using TodoLists.Application.UseCases.UpdateItem;
using TodoLists.Domain.Entities;

namespace TodoLists.Application.FunctionalTests.TodoItems.Commands;

using static Testing;

public class UpdateTodoItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new UpdateItemCommand { Id = 99, Description = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateTodoItem()
    {
        var userId = await RunAsDefaultUserAsync();

        var itemId = await SendAsync(new AddItemCommand
        {
            Title = "New Item"
        });

        var command = new UpdateItemCommand
        {
            Id = itemId,
            Description = "Updated Item Title"
        };

        await SendAsync(command);

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        //item!.Description.Should().Be(command.Description);
        item?.LastModifiedBy.Should().NotBeNull();
        item?.LastModifiedBy.Should().Be(userId);
        item?.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
