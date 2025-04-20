using TodoLists.Application.Common.Exceptions;
using TodoLists.Application.UseCases.AddItem;
using TodoLists.Application.UseCases.UpdateItem;
using TodoLists.Domain.Entities;

namespace TodoLists.Application.FunctionalTests;

using static Testing;

public class UpdateItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new UpdateItemCommand { Id = 99, Description = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldRequireADescriptionWithAMaximumLengthOf200()
    {
        var addItemCommand = new AddItemCommand
        {
            Title = "Title",
            Description = "Description",
            Category = "Category",
        };

        var itemId = await SendAsync(addItemCommand);

        var updateItemCommand = new UpdateItemCommand
        {
            Id = itemId,
            Description = "DescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescription" +
                          "DescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescription",
        };

        await FluentActions.Invoking(() =>
            SendAsync(updateItemCommand)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldUpdateItem()
    {
        var addItemCommand = new AddItemCommand
        {
            Title = "Title",
            Description = "Description",
            Category = "Category",
        };

        var itemId = await SendAsync(addItemCommand);

        var updateItemCommand = new UpdateItemCommand
        {
            Id = itemId,
            Description = "Description modified"
        };

        await SendAsync(updateItemCommand);

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item?.Id.Should().Be(itemId);
        item?.Title.Should().Be(addItemCommand.Title);
        item?.Description.Should().Be(updateItemCommand.Description);
        item?.Category.Should().Be(addItemCommand.Category);
    }
}
