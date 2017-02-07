using System.Collections.Generic;

namespace LCARS.ViewModels.Deployments
{
    public class Project
    {
        public Project()
        {
            Deployments = new List<Deployment>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Group { get; set; }

        public List<Deployment> Deployments { get; set; }
    }
}