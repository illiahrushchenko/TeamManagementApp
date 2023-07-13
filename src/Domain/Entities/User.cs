namespace Domain.Entities;

public class User
{
    public IList<Workspace> Workspaces { get; set; } = new List<Workspace>();
    public IList<Board> Boards { get; set; } = new List<Board>();
}