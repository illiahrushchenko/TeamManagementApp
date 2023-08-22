using FluentValidation;

namespace Application.TableCards.Commands.CreateTableCard;

public class CreateTableCardCommandValidator : AbstractValidator<CreateTableCardCommand>
{
    public CreateTableCardCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}