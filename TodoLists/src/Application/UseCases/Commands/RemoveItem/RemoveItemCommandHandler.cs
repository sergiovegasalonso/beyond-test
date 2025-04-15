using TodoLists.Application.Common.Interfaces;

namespace TodoLists.Application.UseCases.RemoveItem;

public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand>
{
    private readonly IApplicationDbContext _context;

    public RemoveItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.TodoItems.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
