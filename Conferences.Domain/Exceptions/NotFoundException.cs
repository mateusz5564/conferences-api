namespace Conferences.Domain.Exceptions
{
    public class NotFoundException(string resourceType, string resourseIdenifier)
        : Exception($"{resourceType} with id: {resourseIdenifier} not found.")
    {
    }
}
