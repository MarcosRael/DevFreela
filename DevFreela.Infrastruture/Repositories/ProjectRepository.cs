using DevFreela.Core.Repositorios;
using DevFreela.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;

namespace DevFreela.Infrastruture.Repositories
{
    public class ProjectRepository : IProjectRepository
    {

        private readonly string? _connectionString;

        public ProjectRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<List<Project>> GetAll()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "SELECT Id, Title, CreateAt FROM Project";

                var projects = await sqlConnection.QueryAsync<Project>(script);

                return projects.ToList();
            }
        }
    }
}
