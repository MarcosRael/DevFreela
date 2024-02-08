﻿using DevFreeka.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreeka.Core.Entities
{
    public class Project : BaseEntity
    {

        public Project(string title, string description, int idClient, int idFreelancer, decimal totalCost)
        {
            this.Title = title;
            this.Description = description;
            this.IdClient = idClient;
            this.IdFreelancer = idFreelancer;
            this.TotalCost = totalCost;

            this.CreatedAt = DateTime.Now;
            this.Comments = new List<ProjectComment>();
            this.Status = ProjectStatusEnum.Created;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public int IdClient { get; private set; }

        public int IdFreelancer { get; private set; }

        public decimal TotalCost { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? StartedAt { get; private set; }

        public DateTime? FinishedAt { get; private set; }

        public ProjectStatusEnum Status { get; private set; }

        public List<ProjectComment> Comments { get; private set; }


    }
}