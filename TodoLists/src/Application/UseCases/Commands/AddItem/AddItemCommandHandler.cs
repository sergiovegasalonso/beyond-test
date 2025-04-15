using TodoLists.Application.Common.Interfaces;
using TodoLists.Domain.Entities;

namespace TodoLists.Application.UseCases.AddItem;

public class AddItemCommandHandler : IRequestHandler<AddItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public AddItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItem()
        {
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
        };

        var todoItemCreated = _context.TodoItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        _context.Progressions.Add(new Progression { TodoItemId = todoItemCreated.Entity.Id, Date = DateTime.UtcNow, Percent = 0 });

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
