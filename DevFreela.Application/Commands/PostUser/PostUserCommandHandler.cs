using DevFreeka.Core.Entities;
using DevFreela.Infrastruture.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.PostUser
{
    public class PostUserCommandHandler : IRequestHandler<PostUserCommand, int>
    {

        private readonly DevFreelaDbContext _dbContext;

        public PostUserCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(PostUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.FullName,
                                request.Email,
                                request.BirthDate);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }
    }
}
