using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Member> Members { get; }
    DbSet<Invitation> Invitations { get; }
    DbSet<Board> Boards { get; }
    DbSet<Table> Tables { get; }
    DbSet<TableCard> TableCards { get; }
    
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}