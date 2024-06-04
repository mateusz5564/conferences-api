using Conferences.Application.Common;
using Conferences.Application.Conferences.Dtos;
using Conferences.Domain.Constants;
using MediatR;

namespace Conferences.Application.Conferences.Queries.GetAllConferences
{
    public class GetAllConferencesQuery : IRequest<PagedResult<ConferenceDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }

    }
}
