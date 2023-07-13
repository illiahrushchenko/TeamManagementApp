using Domain.Common;

namespace Domain.Entities;

public class TableCard : EntityBase
{
    public User AddedBy { get; set; } = null!;
    public string? Title { get; set; }
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
}