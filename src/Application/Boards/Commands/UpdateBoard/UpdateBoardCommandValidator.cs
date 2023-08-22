using FluentValidation;

namespace Application.Boards.Commands.UpdateBoard;

public class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
{
    public UpdateBoardCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}