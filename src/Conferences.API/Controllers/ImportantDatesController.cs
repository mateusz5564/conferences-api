﻿using Conferences.Application.ImportantDates.Commands.CreateImportantDate;
using Conferences.Application.ImportantDates.Commands.DeleteImportantDateForConferenceById;
using Conferences.Application.ImportantDates.Dtos;
using Conferences.Application.ImportantDates.Queries.GetAllImportantDatesForConference;
using Conferences.Application.ImportantDates.Queries.GetImportantDateForConferenceById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conferences.API.Controllers
{
    [Route("api/conferences/{conferenceId}/important-dates")]
    [ApiController]
    [Authorize]
    public class ImportantDatesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ImportantDateDto>>> GetAllForConference(
            [FromRoute] int conferenceId)
        {
            var importantDates = await mediator
                .Send(new GetImportantDatesForConferenceQuery(conferenceId));

            return Ok(importantDates);
        }

        [HttpGet("{importantDateId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImportantDateDto>> GetForConferenceById(
            [FromRoute] int conferenceId, [FromRoute] int importantDateId)
        {
            var importantDate = await mediator
                .Send(new GetImportantDateForConferenceByIdQuery(conferenceId,
                importantDateId));

            return Ok(importantDate);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] int conferenceId,
            [FromBody] CreateImportantDateCommand command)
        {
            command.ConferenceId = conferenceId;
            var importantDateId = await mediator.Send(command);

            return CreatedAtAction(nameof(GetForConferenceById), 
                new { conferenceId, importantDateId }, null);
        }

        [HttpDelete("{importantDateId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteForConferenceById(
            [FromRoute] int conferenceId, [FromRoute] int importantDateId)
        {
            await mediator
                .Send(new DeleteImportantDateForConferenceByIdCommand(conferenceId,
                importantDateId));

            return NoContent();
        }

    }
}

