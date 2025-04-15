namespace TodoLists.Application.UseCases.RegisterProgression;

public record RegisterProgressionCommand : IRequest
{
    public int TodoItemId { get; set; }

    public DateTime Date { get; set; }

    public decimal Percent { get; set; }
}
