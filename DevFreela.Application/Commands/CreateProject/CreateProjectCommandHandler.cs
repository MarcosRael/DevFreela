﻿using DevFreela.Core.Entities;
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

        public CreateProjectCommandHandler(IProjectRepository projectRepository) => _projectRepository = projectRepository;

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
