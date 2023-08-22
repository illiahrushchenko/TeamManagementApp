using Application.Boards.Commands.CreateBoard;
using FluentValidation;

namespace Application.Tables.Commands.CreateTable;

public class CrateTableCommandValidator : AbstractValidator<CreateTableCommand>
{
    public CrateTableCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);
    }
}