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

namespace DevFreela.Application.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string? _connectionString;

        public UpdateProjectCommandHandler(DevFreelaDbContext dbContext, IConfiguration configuration)    
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == request.Id);

            project?.Update(request.Title, request.Description, request.TotalCost);

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Title = @title, Description = @description, TotalCost = @totalcost WHERE @Id = @id";

                await sqlConnection.ExecuteAsync(script, new { title = project.Title, description = project.Description, totalcost = project.TotalCost, id = project.Id });
            }

            return Unit.Value;
        }
    }
}
