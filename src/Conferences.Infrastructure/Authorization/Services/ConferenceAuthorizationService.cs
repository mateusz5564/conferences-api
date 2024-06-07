using Conferences.Application.User;
using Conferences.Domain.Constants;
using Conferences.Domain.Entities;
using Conferences.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Conferences.Infrastructure.Authorization.Services
{
    public class ConferenceAuthorizationService(ILogger<ConferenceAuthorizationService> logger,
        IUserContext userContext) : IConferenceAuthorizationService
    {
        public bool Authorize(Conference conference, ResourceOperation resourceOperation)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("Authorizing user {email} to {resourceOperation}" +
                " conference {conferenceId}",
                user.Email,
                resourceOperation,
                conference.Id);

            if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }

            if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Admin user, delete operation - successful authorization");
                return true;
            }

            if ((resourceOperation == ResourceOperation.Update || resourceOperation == ResourceOperation.Delete)
                && user.Id == conference.OwnerId)
            {
                logger.LogInformation("Conference owner - successful authorization");
                return true;
            }

            return false;
        }
    }
}
