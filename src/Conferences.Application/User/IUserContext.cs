namespace Conferences.Application.User
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}