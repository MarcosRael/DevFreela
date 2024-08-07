using DevFreela.Application.ViewModels;
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

        private readonly DevFreelaDbContext _dbContext;
        private readonly string? _connectionString;

        public GetByIdProjectQueryHandler(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            //_connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<ProjectDetailsViewModel> Handle(GetByIdProjectQuery request, CancellationToken cancellationToken)
        {
            //TODO: Implement Dapper this query

            var project = await _dbContext.Projects
                                .Include(p => p.Client)
                                .Include(p => p.Freelancer)
                                .SingleOrDefaultAsync(p => p.Id == request.Id);

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
