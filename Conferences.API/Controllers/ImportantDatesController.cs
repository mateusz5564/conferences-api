using Conferences.Application.ImportantDates.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Conferences.API.Controllers
{
    [Route("api/conferences/{conferenceId}/important-dates")]
    [ApiController]
    public class ImportantDatesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] int conferenceId, 
            [FromBody] CreateImportantDateCommand command)
        {
            command.ConferenceId = conferenceId;
            await mediator.Send(command);

            return Created();
        }
    }
}

