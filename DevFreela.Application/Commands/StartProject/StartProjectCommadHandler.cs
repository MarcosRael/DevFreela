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
using DevFreela.Core.Repositories;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommadHandler : IRequestHandler<StartProjectCommad, Unit>
    {
        private readonly IProjectRepository _projectRepository;

        public StartProjectCommadHandler(IProjectRepository projectRepository) => _projectRepository = projectRepository;

        public async Task<Unit> Handle(StartProjectCommad request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project?.Start();

            await _projectRepository.StartAsync(project);
           
            return Unit.Value;
        }
    }
}
