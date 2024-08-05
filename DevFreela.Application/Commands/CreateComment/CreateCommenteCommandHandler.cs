using Dapper;
using DevFreeka.Core.Entities;
using DevFreela.Infrastruture.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
    {

        private readonly DevFreelaDbContext _dbContext;
        private readonly string? _connectionString;

        public CreateCommentCommandHandler(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
            
        }

        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new ProjectComment(request.Content,
                                             request.IdProject,
                                             request.IdUser);

            _dbContext.ProjectComments.Add(comment);

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "INSERT INTO ProjectComments (Content, IdProject, IdUser)" +
                    "VALUE (@content, @idproject, @iduser)";

                await sqlConnection.ExecuteAsync(script, new { content = comment.Content, idproject = comment.IdProject, iduser = comment.IdUser });
            }

            return Unit.Value;
        }
    }
}
