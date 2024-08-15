using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetByIdProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Queries.GetAllProject;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator) => _mediator = mediator;


        [HttpGet]
        public async Task<IActionResult> GetAll(string sQuery)
        {
            var query = new GetAllProjectQuery(sQuery);

            var projects = await _mediator.Send(query);

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetByIdProjectQuery(id);

            var project = await _mediator.Send(query);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteProjectCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(StartProjectCommad command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public async Task<IActionResult> Finish(FinishProjectCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
