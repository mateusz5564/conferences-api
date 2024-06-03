using AutoMapper;
using Conferences.Application.Common;
using Conferences.Application.Conferences.Dtos;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.Conferences.Queries.GetAllConferences
{
    public class GetAllConferencesHandler(ILogger<GetAllConferencesHandler> logger, IMapper mapper,
        IConferencesRepository conferencesRepository) : IRequestHandler<GetAllConferencesQuery,
            PagedResult<ConferenceDto>>
    {
        public async Task<PagedResult<ConferenceDto>> Handle(GetAllConferencesQuery request,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all conferences");

            var (conferences, totalConferences) = await conferencesRepository
                .GetAllMatchingAsync(request.SearchPhrase, request.PageSize, request.PageNumber);
            var conferencesDto = mapper.Map<IEnumerable<ConferenceDto>>(conferences);

            var pagedConferencesDto = new PagedResult<ConferenceDto>(conferencesDto,
                totalConferences, 
                request.PageSize,
                request.PageNumber);

            return pagedConferencesDto;
        }
    }
}
