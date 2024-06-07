using Conferences.Domain.Constants;
using Conferences.Domain.Entities;

namespace Conferences.Domain.Interfaces
{
    public interface IConferenceAuthorizationService
    {
        bool Authorize(Conference conference, ResourceOperation resourceOperation);
    }
}