using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastruture.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    public class SkillController : ControllerBase
    {

        private readonly IMediator _mediator;
        //private readonly ISkillService _skillService;

        public SkillController(IMediator mediator)
        {
            _mediator = mediator;
            //_skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSkillsQuery();

            var skills = await _mediator.Send(query);
                   
            return Ok(skills);
        }
    }
}
