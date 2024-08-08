using DevFreela.Core.Entities;
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

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {

        private readonly DevFreelaDbContext _dbContext;
        private readonly string? _connectionString;

        public CreateProjectCommandHandler(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Title,
                                     request.Description,
                                     request.IdClient,
                                     request.IdFreelancer,
                                     request.TotalCost);

            _dbContext.Projects.Add(project);

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
    }
}
