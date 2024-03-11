using Domain.Common;

namespace Domain.Entities;

public class Member : EntityBase
{
    public User User { get; set; }
    public int UserId { get; set; }
    
    public Board Board { get; set; }
    public int BoardId { get; set; }

    public bool IsAllowedToSendInvitations { get; set; }
    public bool IsAllowedToChangeBoard { get; set; }
}