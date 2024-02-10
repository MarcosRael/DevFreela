using DevFreeka.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.ViewModels
{
    public class UserDetailsViewModel
    {
        public UserDetailsViewModel(int id, string fullName, string email, DateTime birthDate, bool active, List<UserSkill> skills, List<Project> ownedProjects, List<Project> freelanceProjects)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Active = active;
            Skills = skills;
            OwnedProjects = ownedProjects;
            FreelanceProjects = freelanceProjects;
        }

        public int Id { get; private set; }
        public string FullName { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public bool Active { get; private set; }

        public List<UserSkill> Skills { get; private set; }

        public List<Project> OwnedProjects { get; private set; }

        public List<Project> FreelanceProjects { get; private set; }

    }
}
