namespace TodoLists.Application.UseCases.RemoveItem;

public record RemoveItemCommand : IRequest
{
    public int Id { get; set; }
}
