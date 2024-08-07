using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.PostUser
{
    public class PostUserCommand : IRequest<int>
    {
        public string FullName { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
