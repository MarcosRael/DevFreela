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
using DevFreela.Core.Repositories;

namespace DevFreela.Application.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;
        //private readonly DevFreelaDbContext _dbContext;
        //private readonly string? _connectionString;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository)    
        {
            _projectRepository = projectRepository;
            //_dbContext = dbContext;
            //_connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project?.Update(request.Title, request.Description, request.TotalCost);

            await _projectRepository.SaveChangeAsync(project);
            
            return Unit.Value;
        }
    }
}
