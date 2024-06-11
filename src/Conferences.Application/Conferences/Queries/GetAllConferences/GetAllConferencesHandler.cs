using AutoMapper;
using Conferences.Application.Common;
using Conferences.Application.Conferences.Dtos;
using Conferences.Domain.Interfaces;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.Conferences.Queries.GetAllConferences
{
    public class GetAllConferencesHandler(ILogger<GetAllConferencesHandler> logger, IMapper mapper,
        IConferencesRepository conferencesRepository,
        IBlobStorageService blobStorageService) : IRequestHandler<GetAllConferencesQuery,
            PagedResult<ConferenceDto>>
    {
        public async Task<PagedResult<ConferenceDto>> Handle(GetAllConferencesQuery request,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all conferences");

            var (conferences, totalConferences) = await conferencesRepository
                .GetAllMatchingAsync(request.SearchPhrase, request.PageSize, request.PageNumber,
                request.SortBy, request.SortDirection);
            var conferencesDto = mapper.Map<IEnumerable<ConferenceDto>>(conferences);

            foreach (var conference in conferencesDto)
            {
                conference.LogoUrl = blobStorageService.GetBlobSasUrl(conference.LogoUrl);
            }

            var pagedConferencesDto = new PagedResult<ConferenceDto>(conferencesDto,
                totalConferences, 
                request.PageSize,
                request.PageNumber);

            return pagedConferencesDto;
        }
    }
}
