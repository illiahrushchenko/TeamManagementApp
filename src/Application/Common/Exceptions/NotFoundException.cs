namespace Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName, object id)
        : base($"Entity {entityName} with id {id} not found!")
    {
        
    }
}