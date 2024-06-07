using Conferences.Application.Conferences.Dtos;
using MediatR;

namespace Conferences.Application.Conferences.Queries.GetConferenceById
{
    public class GetConferenceByIdQuery(int id) : IRequest<ConferenceDto>
    {
        public int Id { get; } = id;
    }
}
