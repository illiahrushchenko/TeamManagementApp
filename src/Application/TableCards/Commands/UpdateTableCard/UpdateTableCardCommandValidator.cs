using FluentValidation;

namespace Application.TableCards.Commands.UpdateTableCard;

public class UpdateTableCardCommandValidator : AbstractValidator<UpdateTableCardCommand>
{
    public UpdateTableCardCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}