using Conferences.Application.Conferences.Dtos;
using MediatR;

namespace Conferences.Application.Conferences.Queries.GetAllConferences
{
    public class GetAllConferencesQuery : IRequest<IEnumerable<ConferenceDto>>
    {
    }
}
