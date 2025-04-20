using TodoLists.Application.UseCases.AddItem;
using TodoLists.Application.UseCases.GetItems;
using TodoLists.Domain.Entities;

namespace TodoLists.Application.FunctionalTests;

using static Testing;

public class GetItemsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldGetItems()
    {
        var firstAddItemCommand = new AddItemCommand
        {
            Title = "1 Title",
            Description = "First Description",
            Category = "First Category",
        };

        var firstItemId = await SendAsync(firstAddItemCommand);

        var secondAddItemCommand = new AddItemCommand
        {
            Title = "2 Title",
            Description = "Second Description",
            Category = "Second Category",
        };

        var secondItemId = await SendAsync(secondAddItemCommand);

        var items = await SendAsync(new GetItemsQuery());

        items.Should().NotBeNull();
        items.Should().HaveCount(2);
        items?.First().Id.Should().Be(firstItemId);
        items?.First().Title.Should().Be(firstAddItemCommand.Title);
        items?.First().Description.Should().Be(firstAddItemCommand.Description);
        items?.First().Category.Should().Be(firstAddItemCommand.Category);
        items?.Last().Id.Should().Be(secondItemId);
        items?.Last().Title.Should().Be(secondAddItemCommand.Title);
        items?.Last().Description.Should().Be(secondAddItemCommand.Description);
        items?.Last().Category.Should().Be(secondAddItemCommand.Category);
    }
}
