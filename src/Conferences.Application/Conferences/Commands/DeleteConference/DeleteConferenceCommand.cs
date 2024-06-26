﻿using MediatR;

namespace Conferences.Application.Conferences.Commands.DeleteConference
{
    public class DeleteConferenceCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
