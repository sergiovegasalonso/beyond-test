using TodoLists.Application.Common.Interfaces;

namespace TodoLists.Application.UseCases.AddItem;

public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
    private readonly IApplicationDbContext _context;

    public AddItemCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");

        RuleFor(v => v.Description)
            .NotEmpty()
            .MaximumLength(200);
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return !await _context.TodoItems
            .AnyAsync(l => l.Title == title, cancellationToken);
    }
}
