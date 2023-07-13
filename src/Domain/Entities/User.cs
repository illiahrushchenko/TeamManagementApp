namespace Domain.Entities;

public class User
{
    public IList<Board> Boards { get; set; } = new List<Board>();
}