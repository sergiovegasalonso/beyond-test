namespace TodoLists.Application.UseCases.UpdateItem;

public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(v => v.Description)
            .NotEmpty()
            .MaximumLength(200);
    }
}