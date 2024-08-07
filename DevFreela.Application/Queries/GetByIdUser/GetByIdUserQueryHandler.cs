using Azure.Core;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastruture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetByIdUser
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserDetailsViewModel>
    {

        private readonly DevFreelaDbContext _dbContext;

        public GetByIdUserQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDetailsViewModel> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.Id);

            var userDetailsViewModel = new UserDetailsViewModel(
                user.Id,
                user.FullName,
                user.Email,
                user.BirthDate,
                user.Active,
                user.Skills,
                user.OwnedProjects,
                user.FreelanceProjects
                );

            return userDetailsViewModel;
        }
    }
}
