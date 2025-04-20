using TodoLists.Application.Common.Exceptions;
using TodoLists.Application.UseCases.AddItem;
using TodoLists.Domain.Entities;

namespace TodoLists.Application.FunctionalTests;

using static Testing;

public class AddItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new AddItemCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        var firstCommand = new AddItemCommand
        {
            Title = "Same Title",
            Description = "First Description",
            Category = "First Category",
        };

        await SendAsync(firstCommand);

        var secondCommand = new AddItemCommand
        {
            Title = "Same Title",
            Description = "Second Description",
            Category = "Second Category",
        };

        await FluentActions.Invoking(() =>
            SendAsync(secondCommand)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireATitleWithAMaximumLengthOf200()
    {
        var command = new AddItemCommand
        {
            Title = "TitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitle" +
                    "TitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitle",
            Description = "Description",
            Category = "Category",
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireADescriptionWithAMaximumLengthOf200()
    {
        var command = new AddItemCommand
        {
            Title = "Title",
            Description = "DescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescription" +
                          "DescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescriptionDescription",
            Category = "Category",
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldAddItem()
    {
        var command = new AddItemCommand
        {
            Title = "Title",
            Description = "Description",
            Category = "Category",
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item?.Title.Should().Be(command.Title);
        item?.Description.Should().Be(command.Description);
        item?.Category.Should().Be(command.Category);
    }
}
