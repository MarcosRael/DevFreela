using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Infrastruture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetByIdProject
{
    public class GetByIdProjectQueryHandler : IRequestHandler<GetByIdProjectQuery, ProjectDetailsViewModel>
    {

        private readonly IProjectRepository _projectRepository;

        public GetByIdProjectQueryHandler(IProjectRepository projectRepository) => _projectRepository = projectRepository;

        public async Task<ProjectDetailsViewModel> Handle(GetByIdProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            var projectDetailsViewModel = new ProjectDetailsViewModel(
                project.Id,
                project.Title,
                project.Description,
                project.TotalCost,
                project.StartedAt,
                project.CreatedAt,
                project.Client.FullName,
                project.Freelancer.FullName
                );

            return projectDetailsViewModel;
        }
    }
}
