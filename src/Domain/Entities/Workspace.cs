using Domain.Common;

namespace Domain.Entities;

public class Workspace : EntityBase
{
    public string? Title { get; set; }
    public IList<Board> Boards { get; set; } = new List<Board>();
    public IList<User> Members { get; set; } = new List<User>();
}