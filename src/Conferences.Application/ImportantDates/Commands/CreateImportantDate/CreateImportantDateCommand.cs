﻿using MediatR;

namespace Conferences.Application.ImportantDates.Commands.CreateImportantDate
{
    public class CreateImportantDateCommand() : IRequest<int>
    {
        public string Name { get; set; } = default!;
        public DateTime Date { get; set; }
        public int ConferenceId { get; set; }
    }
}
