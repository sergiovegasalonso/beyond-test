namespace TodoLists.Application.UseCases.UpdateItem;

public record UpdateItemCommand : IRequest
{
    public int Id { get; set; }

    public string? Description { get; set; }
}