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
            Id = 0,

        }; // todo: fix

        entity.Title = request.Title;

        _context.TodoItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
