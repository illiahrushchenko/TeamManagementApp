namespace Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException(string entityName, object id) 
        : base($"Forbidden access to {entityName} with id {id}")
    {
    }
}