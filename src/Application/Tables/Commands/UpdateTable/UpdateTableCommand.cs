using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tables.Commands.UpdateTable;

public record UpdateTableCommand(int TableId, string Title, int BoardId) : IRequest<int>;

public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateTableCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        var table = await _context.Tables
                        .FirstOrDefaultAsync(x => x.Id == request.TableId, cancellationToken: cancellationToken) ??
                    throw new NotFoundException(nameof(Table), request.TableId);

        table.BoardId = request.BoardId;
        table.Title = request.Title;

        _context.Tables.Update(table);
        await _context.SaveChangesAsync(cancellationToken);

        return table.Id;
    }
}