using TodoLists.Application.Common.Interfaces;

namespace TodoLists.Application.UseCases.RegisterProgression;

public class RegisterProgressionCommandValidator : AbstractValidator<RegisterProgressionCommand>
{
    private readonly IApplicationDbContext _context;

    public RegisterProgressionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Percent)
            .NotEmpty()
            .InclusiveBetween(0, 100)
                .WithMessage("Percent must bet between 0 and 100 (both inclusive).")
                .WithErrorCode("Generic");

        RuleFor(v => v)
            .MustAsync((command, cancellationToken) => LastProgressionDateMustBeLowerThanNew(command.TodoItemId, command.Date, cancellationToken))
                .WithMessage("The new progression date must be later than the last progression date for the todo item.")
                .WithErrorCode("Generic")
            .MustAsync((command, cancellationToken) => PercentShouldNotExcedeed100AfterAdded(command.TodoItemId, command.Percent, cancellationToken))
                .WithMessage("Percent should not excedeed 100.")
                .WithErrorCode("Generic");
    }

    public async Task<bool> LastProgressionDateMustBeLowerThanNew(int todoItemId, DateTime newDate, CancellationToken cancellationToken)
    {
        var lastProgression = await _context.Progressions
            .Where(x => x.TodoItemId == todoItemId)
            .OrderByDescending(x => x.Date)
            .FirstOrDefaultAsync(cancellationToken);

        if (lastProgression == null)
        {
            return true;
        }

        return newDate > lastProgression.Date;
    }

    public async Task<bool> PercentShouldNotExcedeed100AfterAdded(int todoItemId, decimal percentToAdd, CancellationToken cancellationToken)
    {
        var lastProgression = await _context.Progressions
                .Where(x => x.TodoItemId == todoItemId)
                .OrderByDescending(x => x.Date)
                .FirstOrDefaultAsync(cancellationToken);

        if (lastProgression == null)
        {
            return true;
        }

        return percentToAdd + lastProgression.Percent <= 100;
    }
}
