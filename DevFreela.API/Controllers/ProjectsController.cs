﻿using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly OpeningTimeOption _option;

        public ProjectsController(IOptions<OpeningTimeOption> option, ExampleLifeTimeObject exampleLifeTime)
        {
            exampleLifeTime.Example = "updated at ProjectController";
            _option = option.Value;   
        }

        // api/projects?query=net core
        [HttpGet]
        public IActionResult GetAll(string query)
        {
            // Buscar todos ou filtrar

            return Ok();
        }

        // api/projects/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Buscar o projeto

            // return NotFound();

            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectModel createProject)
        {
            if(createProject.Title.Length > 50)
            {
                return BadRequest();
            }

            // Cadastrar projeto
            return CreatedAtAction(nameof(GetById), new { id = createProject.id }, createProject);

        }

        // api/projects/2
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectModel updateProject)
        {
            if(updateProject.Description.Length > 200)
            {
                return BadRequest();
            }

            // Atualizar o objeto

            return NoContent();

        }

        // api/projects/3
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            // Buscar, se não existir, retorna NotFound()


            // Remover

            return NoContent();

        }

        // api/projects/1/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateCommentModel createComment)
        {
            return NoContent();
        }

        // api/projects/1/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        // api/projects/1/finish
        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            return NoContent();
        }
    }
}
