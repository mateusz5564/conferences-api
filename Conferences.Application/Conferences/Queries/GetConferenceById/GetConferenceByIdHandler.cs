using AutoMapper;
using Conferences.Application.Conferences.Dtos;
using Conferences.Domain.Repositories;
using MediatR;

namespace Conferences.Application.Conferences.Queries.GetConferenceById
{
    public class GetConferenceByIdHandler(IMapper mapper, IConferencesRepository conferencesRepository) : IRequestHandler<GetConferenceByIdQuery, ConferenceDto>
    {
        public async Task<ConferenceDto> Handle(GetConferenceByIdQuery request, CancellationToken cancellationToken)
        {
            var conference = await conferencesRepository.GetByIdAsync(request.Id);

            var conferenceDto = mapper.Map<ConferenceDto>(conference);

            return conferenceDto;
        }
    }
}
