using Domain.Common;

namespace Domain.Entities;

public class Invitation : EntityBase
{
    public User User { get; set; }
    public int UserId { get; set; }
    
    public Board Board { get; set; }
    public int BoardId { get; set; }
}