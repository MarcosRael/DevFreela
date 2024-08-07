using DevFreela.API.Models;
using DevFreela.Application.Commands.PostUser;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetByIdUser;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.ComponentModel.Design;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMediator _mediator;

        public UsersController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            //var user = _userService.GetById(id);

            var query = new GetByIdUserQuery(id);

            var user = _mediator.Send(query);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostUserCommand command)
        {
            //var id = _userService.Create(inputModel);

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
