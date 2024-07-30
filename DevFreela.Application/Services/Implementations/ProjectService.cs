using Dapper;
using DevFreeka.Core.Entities;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastruture.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string? _connectionString;

        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public int Create(NewProjectInputModel inputModel)
        {
            var project = new Project(inputModel.Title,
                                      inputModel.Description,
                                      inputModel.IdClient,
                                      inputModel.IdFreelancer,
                                      inputModel.TotalCost);

            _dbContext.Projects.Add(project);

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "INSERT INTO Project (Title, Description, IdClient, IdFreelancer, TotalCost) " +
                    "VALUE (@title, @description, @idclient, @idfreelancer, @totalcost)";

                return sqlConnection.ExecuteScalar<int>(script, new { title = project.Title
                                                    ,description = project.Description
                                                    ,idclient = project.IdClient
                                                    ,idfreelancer = project.IdFreelancer
                                                    ,totalcost = project.TotalCost});
            }

        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content,
                                             inputModel.IdProject,
                                             inputModel.IdUser);

            _dbContext.ProjectComments.Add(comment);

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "INSERT INTO ProjectComments (Content, IdProject, IdUser)" +
                    "VALUE (@content, @idproject, @iduser)";

                sqlConnection.Execute(script, new { content = comment.Content, idproject = comment.IdProject, iduser = comment.IdUser});
            }

        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project?.Cancel();


            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "DELETE FROM Project WHERE Id = @id";

                sqlConnection.Execute(script, new {id});
            }
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project?.Finish();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Project SET Status = @status, FinishedAt = @finishedat WHERE Id = @id";

                sqlConnection.Execute(script, new { status = project.Status, finishedat = project.FinishedAt, id});
            }
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            var projetcs = _dbContext.Projects;

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "SELECT Id, Title, CreateAt FROM Project";

                return sqlConnection.Query<ProjectViewModel>(script).ToList();
            }
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            //TODO: Implement Dapper this query

            var project = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefault(p => p.Id == id);

            var projectDetailsViewModel = new ProjectDetailsViewModel(
                project.Id,
                project.Title,
                project.Description,
                project.TotalCost,
                project.StartedAt,
                project.CreatedAt,
                project.Client.FullName,
                project.Freelancer.FullName
                );

              return projectDetailsViewModel;
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project?.Start();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE Id = @Id";

                sqlConnection.Execute(script, new { status = project.Status, startedat = project.StartedAt, id });

            }
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);

            project?.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Title = @title, Description = @description, TotalCost = @totalcost WHERE @Id = @id";

                sqlConnection.Execute(script, new { title = project.Title, description = project.Description, totalcost = project.TotalCost, id = project.Id});
            }
        }
    }
}
