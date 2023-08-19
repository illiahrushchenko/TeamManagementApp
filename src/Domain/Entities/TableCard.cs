using Domain.Common;

namespace Domain.Entities;

public class TableCard : EntityBase
{
    public Table Table { get; set; } = null!;
    public int TableId { get; set; }

    public User AddedBy { get; set; } = null!;
    public int AddedById { get; set; }
    
    public string? Title { get; set; }
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
}