using TodoLists.Application.Common.Interfaces;
using TodoLists.Application.UseCases.RemoveItem;

namespace BeyondTest.Application.UseCases.Commands.RemoveItem;

public class RemoveItemCommandValidator : AbstractValidator<RemoveItemCommand>
{
    private readonly IApplicationDbContext _context;

    public RemoveItemCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Id)
            .MustAsync(LastProgressionDoesNotHaveMoreThan50PercentCompleted)
                .WithMessage("Todo item with '{PropertyName}' id must not have last progression with more than 50 percent completed.")
                .WithErrorCode("Generic");
    }

    public async Task<bool> LastProgressionDoesNotHaveMoreThan50PercentCompleted(int id, CancellationToken cancellationToken)
    {
        var lastProgression = await _context.Progressions
            .Where(x => x.TodoItemId == id)
            .OrderByDescending(x => x.Date)
            .FirstOrDefaultAsync(cancellationToken);

        if (lastProgression == null)
        {
            return true;
        }

        return lastProgression.Percent <= 50;
    }
}
