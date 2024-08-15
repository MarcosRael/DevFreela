using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Queries.GetByIdUser;
using Microsoft.AspNetCore.Mvc;
using DevFreela.API.Models;
using MediatR;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var query = new GetByIdUserQuery(id);

            var user = _mediator.Send(query);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUserCommand command)
        {
            var id = _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        [HttpPut]
        public IActionResult Login(int id, [FromBody] LoginModel login)
        {
            return NoContent();
        }

    }
}
