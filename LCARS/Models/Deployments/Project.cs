using System.Collections.Generic;

namespace LCARS.Models.Deployments
{
    public class Project
    {
        public Project()
        {
            Deployments = new List<Deployment>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ProjectGroupId { get; set; }

        public IList<Deployment> Deployments { get; set; }
    }
}