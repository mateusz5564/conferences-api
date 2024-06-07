using AutoMapper;
using Conferences.Application.ImportantDates.Commands.CreateImportantDate;
using Conferences.Domain.Entities;

namespace Conferences.Application.ImportantDates.Dtos
{
    public class ImportantDatesProfile : Profile
    {
        public ImportantDatesProfile()
        {
            CreateMap<CreateImportantDateCommand, ImportantDate>();

            CreateMap<ImportantDate, ImportantDateDto>();
        }
    }
}
