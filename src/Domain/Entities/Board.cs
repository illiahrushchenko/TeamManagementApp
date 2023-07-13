using Domain.Common;

namespace Domain.Entities;

public class Board : EntityBase
{
    public IList<Table> Tables { get; set; } = new List<Table>();
    public IList<User> Members { get; set; } = new List<User>();
    
    public string? Titile { get; set; }
}