using TodoLists.Application.Common.Interfaces;

namespace TodoLists.Application.UseCases.UpdateItem;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        //entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
