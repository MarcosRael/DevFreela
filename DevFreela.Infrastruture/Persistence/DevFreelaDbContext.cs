using DevFreeka.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastruture.Persistence
{
    public class DevFreelaDbContext
    {

        public DevFreelaDbContext()
        {

            Projects = new List<Project>
            {
                new Project("Meu projeto ASPNET Core 1", "Minha descrição de projeto 1", 1, 1, 10000),
                new Project("Meu projeto ASPNET Core 2", "Minha descrição de projeto 2", 1, 1, 20000),
                new Project("Meu projeto ASPNET Core 3", "Minha descrição de projeto 3", 1, 1, 30000)
            };

            Users = new List<User> 
            { 
                new User("Marcos Raél", "marcos.rael@email.com", new DateTime(1991,1,1)),
                new User("Pedro Henrique", "pedro.henrique@email.com", new DateTime(1995,1,1)),
                new User("Leandro Leonardo", "leandro.leonardo@email.com", new DateTime(2000,1,1)),
            
            };

            Skills = new List<Skill> 
            { 
                new Skill(".Net Core"),
                new Skill("C#"),
                new Skill("SQL")
            };
        }

        public List<Project> Projects { get; set; }

        public List<User> Users { get; set; }

        public List<Skill> Skills { get; set; }

    }
}
