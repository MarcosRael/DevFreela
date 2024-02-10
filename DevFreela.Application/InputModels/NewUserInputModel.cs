using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.InputModels
{
    public class NewUserInputModel
    {
        public NewUserInputModel(string fullName, string email, DateTime birthDate)
        {
            this.FullName = fullName;
            this.Email = email;
            this.BirthDate = birthDate;
        }

        public string FullName { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

    }
}
