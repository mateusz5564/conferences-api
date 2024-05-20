﻿using AutoMapper;
using Conferences.Application.Conferences.Dtos;
using Conferences.Domain.Entities;
using Conferences.Domain.Repositories;

namespace Conferences.Application.Conferences
{
    public class ConferencesService(IConferencesRepository conferencesRepository, IMapper mapper) : IConferencesService
    {
        public async Task<IEnumerable<ConferenceDto>> GetAllConferences()
        {
            var conferences = await conferencesRepository.GetAllAsync();
            var conferencesDto = mapper.Map<IEnumerable<ConferenceDto>>(conferences);

            return conferencesDto;
        }

        public async Task<ConferenceDto?> GetConferenceById(int id)
        {
            var conference = await conferencesRepository.GetByIdAsync(id);

            var conferenceDto = mapper.Map<ConferenceDto>(conference);

            return conferenceDto;
        }

        public async Task<int> CreateConference(CreateConferenceDto conferenceDto)
        {
            var conference = mapper.Map<Conference>(conferenceDto);

            var id = await conferencesRepository.CreateAsync(conference);

            return id;
        }

    }
}