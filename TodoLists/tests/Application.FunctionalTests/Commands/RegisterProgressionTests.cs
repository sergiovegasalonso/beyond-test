using TodoLists.Application.Common.Exceptions;
using TodoLists.Application.UseCases.AddItem;
using TodoLists.Application.UseCases.RegisterProgression;
using TodoLists.Domain.Entities;

namespace TodoLists.Application.FunctionalTests;

using static Testing;

public class RegisterProgressionTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new RegisterProgressionCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequirePercentageBeBetween0And100Inclusive()
    {
        var addItemCommand = new AddItemCommand
        {
            Title = "Title",
            Description = "Description",
            Category = "Category",
        };

        var itemId = await SendAsync(addItemCommand);

        var registerProgressionCommand = new RegisterProgressionCommand
        {
            TodoItemId = itemId,
            Date = DateTime.UtcNow.AddDays(1),
            Percent = 500
        };

        await FluentActions.Invoking(() =>
            SendAsync(registerProgressionCommand)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequirePercentageDoesNotExceed100()
    {
        var addItemCommand = new AddItemCommand
        {
            Title = "Title",
            Description = "Description",
            Category = "Category",
        };

        var itemId = await SendAsync(addItemCommand);

        var firstRegisterProgressionCommand = new RegisterProgressionCommand
        {
            TodoItemId = itemId,
            Date = DateTime.UtcNow.AddMinutes(5),
            Percent = 75
        };

        await SendAsync(firstRegisterProgressionCommand);

        var secondRegisterProgressionCommand = new RegisterProgressionCommand
        {
            TodoItemId = itemId,
            Date = DateTime.UtcNow.AddMinutes(1),
            Percent = 26
        };

        await FluentActions.Invoking(() =>
            SendAsync(secondRegisterProgressionCommand)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireNewDateBeGreaterThanLastDate()
    {
        var addItemCommand = new AddItemCommand
        {
            Title = "Title",
            Description = "Description",
            Category = "Category",
        };

        var itemId = await SendAsync(addItemCommand);

        var registerProgressionCommand = new RegisterProgressionCommand
        {
            TodoItemId = itemId,
            Date = DateTime.UtcNow.AddMinutes(-1),
            Percent = 50
        };

        await FluentActions.Invoking(() =>
            SendAsync(registerProgressionCommand)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRegisterProgression()
    {
        var addItemCommand = new AddItemCommand
        {
            Title = "Title",
            Description = "Description",
            Category = "Category",
        };

        var itemId = await SendAsync(addItemCommand);

        var firstRegisterProgressionCommand = new RegisterProgressionCommand
        {
            TodoItemId = itemId,
            Date = DateTime.UtcNow.AddMinutes(5),
            Percent = 50
        };

        await SendAsync(firstRegisterProgressionCommand);

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item?.Id.Should().Be(itemId);
        item?.Title.Should().Be(addItemCommand.Title);
        item?.Description.Should().Be(addItemCommand.Description);
        item?.Category.Should().Be(addItemCommand.Category);
        item?.Progressions?.First().TodoItemId.Should().Be(firstRegisterProgressionCommand.TodoItemId);
        item?.Progressions?.First().Percent.Should().Be(firstRegisterProgressionCommand.Percent);
        item?.Progressions?.First().Date.Should().Be(firstRegisterProgressionCommand.Date);
    }
}
