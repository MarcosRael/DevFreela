using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public ProjectDetailsViewModel(int id, string title, string description, decimal totalCost, DateTime? startedAt, DateTime? createdAt)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.TotalCost = totalCost;
            this.StartedAt = startedAt;
            this.CreatedAt = createdAt;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public decimal TotalCost { get; private set; }

        public DateTime? StartedAt { get; private set; }

        public DateTime? CreatedAt { get; private set; }

    }
}
