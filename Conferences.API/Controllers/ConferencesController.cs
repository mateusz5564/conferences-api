using Conferences.Application.Conferences.Commands.CreateConference;
using Conferences.Application.Conferences.Commands.DeleteConference;
using Conferences.Application.Conferences.Queries.GetAllConferences;
using Conferences.Application.Conferences.Queries.GetConferenceById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Conferences.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferencesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var conferences = await mediator.Send(new GetAllConferencesQuery());

            return Ok(conferences);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var conference = await mediator.Send(new GetConferenceByIdQuery(id));

            if (conference == null)
            {
                return NotFound();
            }

            return Ok(conference);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConferenceCommand command)
        {
            var id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var isDeleted = await mediator.Send(new DeleteConferenceCommand(id));

            if(!isDeleted) return NotFound();

            return NoContent();
        }
    }
}
