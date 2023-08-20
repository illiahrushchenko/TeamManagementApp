using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TableCards.Commands.UpdateTableCard;

public record UpdateTableCardCommand(int TableCardId, string Title, string Description, int TableId) : IRequest<int>;

public class UpdateTableCardCommandHandler : IRequestHandler<UpdateTableCardCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateTableCardCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateTableCardCommand request, CancellationToken cancellationToken)
    {
        var tableCard = await _context.TableCards
                        .FirstOrDefaultAsync(x => x.Id == request.TableCardId, cancellationToken: cancellationToken) ??
                    throw new NotFoundException(nameof(TableCard), request.TableCardId);

        tableCard.TableId = request.TableId;
        tableCard.Title = request.Title;
        tableCard.Description = request.Description;
        

        _context.TableCards.Update(tableCard);
        await _context.SaveChangesAsync(cancellationToken);

        return tableCard.Id;
    }
}