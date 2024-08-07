using DevFreela.Application.ViewModels;
using DevFreela.Infrastruture.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetAllProject
{
    public class GetAllProjectQueryHandler : IRequestHandler<GetAllProjectQuery, List<ProjectViewModel>>
    {
        private readonly string? _connectionString;

        public GetAllProjectQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<List<ProjectViewModel>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "SELECT Id, Title, CreateAt FROM Project";

                var projects = await sqlConnection.QueryAsync<ProjectViewModel>(script);

                return projects.ToList();
            }
        }
    }
}
