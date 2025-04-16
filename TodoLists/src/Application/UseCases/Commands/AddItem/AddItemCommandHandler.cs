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

        var todoItemCreated = await _context.TodoItems.AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        await _context.Progressions.AddAsync(new Progression
        {
            TodoItemId = todoItemCreated.Entity.Id,
            Date = DateTime.UtcNow,
            Percent = 0
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
