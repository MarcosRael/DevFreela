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
using DevFreela.Core.Repositories;
using Azure.Core;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;
        //private readonly DevFreelaDbContext _dbContext;
        //private readonly string? _connectionString;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
            //_dbContext = dbContext;
            //_connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Title,
                                     request.Description,
                                     request.IdClient,
                                     request.IdFreelancer,
                                     request.TotalCost);

           var id = await _projectRepository.AddAsync(project);
                        
           return id;            
        }
    }
}
