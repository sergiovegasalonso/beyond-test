using TodoLists.Domain.Enums;

namespace TodoLists.Application.UseCases.AddItem;

public record AddItemCommand : IRequest<int>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }
}
