using TodoLists.Application.Common.Interfaces;
using TodoLists.Domain.Entities;

namespace TodoLists.Application.UseCases.RegisterProgression;

public class RegisterProgressionCommandHandler : IRequestHandler<RegisterProgressionCommand>
{
    private readonly IApplicationDbContext _context;

    public RegisterProgressionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RegisterProgressionCommand request, CancellationToken cancellationToken)
    {
        var todoItem = await _context.TodoItems
            .FindAsync(new object[] { request.TodoItemId }, cancellationToken);

        Guard.Against.NotFound(request.TodoItemId, todoItem);

        await _context.Progressions.AddAsync(new Progression
        {
            TodoItemId = todoItem.Id,
            Date = DateTime.UtcNow,
            Percent = request.Percent,
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
