using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastruture.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        //private readonly DevFreelaDbContext _dbContext;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
           // _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.FullName,
                                request.Email,
                                request.BirthDate);

            var id = await _userRepository.AddAsync(user);

            return id;
        }
    }
}
