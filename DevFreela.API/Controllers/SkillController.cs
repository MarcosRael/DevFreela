using DevFreela.Application.Queries.GetAllSkills;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    public class SkillController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSkillsQuery();

            var skills = await _mediator.Send(query);

            return Ok(skills);
        }
    }
}
