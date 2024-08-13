using DevFreela.Core.Repositories;
using DevFreela.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;
using DevFreela.Infrastruture.Persistence;
using Azure.Core;

namespace DevFreela.Infrastruture.Repositories
{
    public class ProjectRepository : IProjectRepository
    {

        private readonly DevFreelaDbContext _dbContext;
        private readonly string? _connectionString;

        public ProjectRepository(DevFreelaDbContext dbContext,IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<int> AddAsync(Project project)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "INSERT INTO Project (Title, Description, IdClient, IdFreelancer, TotalCost) " +
                    "VALUE (@title, @description, @idclient, @idfreelancer, @totalcost)";

                return await sqlConnection.ExecuteScalarAsync<int>(script, new
                {
                    title = project.Title
                    ,
                    description = project.Description
                    ,
                    idclient = project.IdClient
                    ,
                    idfreelancer = project.IdFreelancer
                    ,
                    totalcost = project.TotalCost
                });

            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "DELETE FROM Project WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { id });
            }
        }

        public async Task FinishAsync(Project? project)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Project SET Status = @status, FinishedAt = @finishedat WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, finishedat = project.FinishedAt, project.Id });
            }
        }

        public async Task<List<Project>> GetAllAsync()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "SELECT Id, Title, CreateAt FROM Project";

                var projects = await sqlConnection.QueryAsync<Project>(script);

                return projects.ToList();
            }
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            //TODO: Implement Dapper this query
            var project = await _dbContext.Projects
                     .Include(p => p.Client)
                     .Include(p => p.Freelancer)
                     .SingleOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task StartAsync(Project? project)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE Id = @Id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = project.StartedAt, project.Id });
            }
        }

        public async Task SaveChangeAsync(Project? project)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Title = @title, Description = @description, TotalCost = @totalcost WHERE @Id = @id";

                await sqlConnection.ExecuteAsync(script, new { title = project.Title, description = project.Description, totalcost = project.TotalCost, id = project.Id });
            }
        }

        public async Task AddCommentAsync(ProjectComment projectComment)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "INSERT INTO ProjectComments (Content, IdProject, IdUser)" +
                    "VALUE (@content, @idproject, @iduser)";

                await sqlConnection.ExecuteAsync(script, new { content = projectComment.Content, idproject = projectComment.IdProject, iduser = projectComment.IdUser });
            }
        }
    }
}
