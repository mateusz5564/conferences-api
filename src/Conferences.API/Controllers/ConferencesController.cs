using Conferences.Application.Conferences.Commands.CreateConference;
using Conferences.Application.Conferences.Commands.DeleteConference;
using Conferences.Application.Conferences.Commands.UpdateConference;
using Conferences.Application.Conferences.Dtos;
using Conferences.Application.Conferences.Queries.GetAllConferences;
using Conferences.Application.Conferences.Queries.GetConferenceById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conferences.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConferencesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ConferenceDto>>> GetAll(
            [FromQuery] GetAllConferencesQuery query)
        {
            var conferences = await mediator.Send(query);

            return Ok(conferences);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConferenceDto>> GetById([FromRoute] int id)
        {
            var conference = await mediator.Send(new GetConferenceByIdQuery(id));
            return Ok(conference);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConferenceCommand command)
        {
            var id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await mediator.Send(new DeleteConferenceCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateConferenceCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
