﻿using AutoMapper;
using Conferences.Application.ImportantDates.Commands;
using Conferences.Domain.Entities;

namespace Conferences.Application.ImportantDates.Dtos
{
    public class ImportantDatesProfile : Profile
    {
        public ImportantDatesProfile()
        {
            CreateMap<CreateImportantDateCommand, ImportantDate>();
        }
    }
}
