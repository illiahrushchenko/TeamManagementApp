namespace Application.Common.Interfaces;

public interface ICurrentUserService
{
    public string? UserEmail { get; }
    public int UserId { get; }
}