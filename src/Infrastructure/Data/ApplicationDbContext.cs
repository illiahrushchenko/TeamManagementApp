using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Member>(entity =>
        {
            entity.HasOne(p => p.User)
                .WithMany(p => p.Memberships)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // builder.Entity<Board>(entity =>
        // {
        //     // entity.HasMany(p => p.Members)
        //     //     .WithMany(p => p.OtherBoards);
        //
        //     entity.HasOne(p => p.Owner)
        //         .WithMany(p => p.OwnBoards)
        //         .HasForeignKey(p => p.OwnerId)
        //         .OnDelete(DeleteBehavior.Cascade);
        // });

        builder.Entity<TableCard>(entity =>
        {
            entity.HasOne(p => p.AddedBy)
                .WithMany(p => p.AddedCards)
                .HasForeignKey(p => p.AddedById)
                .OnDelete(DeleteBehavior.SetNull);
        });
        base.OnModelCreating(builder);
    }

    public DbSet<Invitation> Invitations => Set<Invitation>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Board> Boards => Set<Board>();
    public DbSet<Table> Tables => Set<Table>();
    public DbSet<TableCard> TableCards => Set<TableCard>();
}