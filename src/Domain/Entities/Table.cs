using Domain.Common;

namespace Domain.Entities;

public class Table : EntityBase
{
    public Board Board { get; set; } = null!;
    public int BoardId { get; set; }
    
    public string? Title { get; set; }

    public IList<TableCard> Cards { get; set; } = new List<TableCard>();
}