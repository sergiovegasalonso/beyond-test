using TodoLists.Domain.Entities;

namespace TodoLists.Application.UseCases.GetItems;

public class TodoItemDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }

    public List<Progression> Progressions { get; set; } = new List<Progression>();

    public bool IsCompleted { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TodoItem, TodoItemDto>();
        }
    }
}
