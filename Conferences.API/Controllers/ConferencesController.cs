using Conferences.Application.Conferences;
using Conferences.Application.Conferences.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Conferences.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferencesController(IConferencesService conferencesService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var conferences = await conferencesService.GetAllConferences();

            return Ok(conferences);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var conference = await conferencesService.GetConferenceById(id);

            if (conference == null)
            {
                return NotFound();
            }

            return Ok(conference);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConferenceDto createConferenceDto)
        {
            var id = await conferencesService.CreateConference(createConferenceDto);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
