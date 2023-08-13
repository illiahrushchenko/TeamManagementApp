namespace Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName, int id)
        : base($"Entity {entityName} with id {id} not found!")
    {
        
    }
}