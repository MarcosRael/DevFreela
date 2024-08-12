using DevFreela.Core.Repositories;
using DevFreela.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;
using DevFreela.Infrastruture.Persistence;

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
    }
}
