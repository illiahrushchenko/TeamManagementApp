using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<int>
{
    public IList<Board> OwnBoards { get; set; } = new List<Board>();
    public IList<Board> OtherBoards { get; set; } = new List<Board>();

    public IList<TableCard> AddedCards { get; set; } = new Collection<TableCard>();

}