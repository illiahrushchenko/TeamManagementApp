using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<int>
{
    public IList<Invitation> Invitations { get; set; } = new List<Invitation>();
    public IList<Member> Memberships { get; set; } = new List<Member>();
    public IList<TableCard> AddedCards { get; set; } = new Collection<TableCard>();
}