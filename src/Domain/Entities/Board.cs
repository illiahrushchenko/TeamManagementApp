using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class Board : EntityBase
{
    public Board(string title)
    {
        Title = title;
    }
    
    public IList<Table> Tables { get; set; } = new List<Table>();
    public IList<Member> Members { get; set; } = new List<Member>();
    public IList<Invitation> Invitations { get; set; } = new List<Invitation>();
    public string? Title { get; set; }
}