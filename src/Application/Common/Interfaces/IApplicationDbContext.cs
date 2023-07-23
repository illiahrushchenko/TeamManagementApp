using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Board> Boards { get; }
    DbSet<Table> Tables { get; }
    DbSet<TableCard> TableCards { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}