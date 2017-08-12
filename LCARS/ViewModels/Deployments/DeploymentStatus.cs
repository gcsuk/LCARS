﻿using System.Collections.Generic;

namespace LCARS.ViewModels.Deployments
{
    public class DeploymentStatus
    {
        public IEnumerable<Deployment> Deployments { get; set; }

        public IList<Project> Projects { get; set; }

        public IList<Environment> Environments { get; set; }
    }
}