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
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.TodoItemId }, cancellationToken);

        Guard.Against.NotFound(request.TodoItemId, entity);

        /*entity.Progressions.Add(new Progression
        {
            Id = Guid.NewGuid(),
            Date = request.Date,
            Percent = request.Percent,
        });*/

        await _context.SaveChangesAsync(cancellationToken);
    }
}
