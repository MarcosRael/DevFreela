using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Services.Implementations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.Validators;
using DevFreela.Core.Repositories;
using DevFreela.Infrastruture.Persistence;
using DevFreela.Infrastruture.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.API
{
    public class Program
    {

        public Program() { }


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs");
            builder.Services.AddDbContext<DevFreelaDbContext>(option => option.UseSqlServer(connectionString));

            //builder.Services.AddScoped<IProjectService, ProjectService>();
            //builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<ISkillService, SkillService>();

            // Add Repository
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            //builder.Services.AddScoped<IUserRepository, UserRepository>();
            //builder.Services.AddScoped<ISkillService, SkillService>();

            // Add services to the container.
            builder.Services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());

            builder.Services.AddMediatR(typeof(CreateProjectCommand));
            //builder.Services.AddMediatR(typeof(CreateUserCommand));
            //builder.Services.AddMediatR(typeof(CreateCommentCommand));
            //builder.Services.AddMediatR(typeof(DeleteProjectCommand));
            //builder.Services.AddMediatR(typeof(FinishProjectCommand));
            //builder.Services.AddMediatR(typeof(StartProjectCommad));
            //builder.Services.AddMediatR(typeof(UpdateProjectCommand));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
