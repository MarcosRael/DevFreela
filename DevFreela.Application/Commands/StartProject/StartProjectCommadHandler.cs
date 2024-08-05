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
using Azure.Core;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommadHandler : IRequestHandler<StartProjectCommad, Unit>
    {

        private readonly DevFreelaDbContext _dbContext;
        private readonly string? _connectionString;

        public StartProjectCommadHandler(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<Unit> Handle(StartProjectCommad request, CancellationToken cancellationToken)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == request.Id);

            project?.Start();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE Id = @Id";

               await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = project.StartedAt, request.Id });
            }

            return Unit.Value;
        }
    }
}
