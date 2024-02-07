using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreeka.Core.Entities
{
    public class Skill : BaseEntity
    {

        public Skill(string description)
        {
            this.Description = description;
            this.CreatedAt = DateTime.Now;
        }

        public string Description { get; private set; }

        public DateTime CreatedAt { get; private set; }


    }
}
