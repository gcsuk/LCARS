using System.Collections.Generic;
using Newtonsoft.Json;

namespace LCARS.Models.Deployments
{
    public class Deployments
    {
        public Deployments()
        {
            Deploys = new List<Deployment>();
        }

        public List<ProjectGroup> ProjectGroups { get; set; }

        public List<Project> Projects { get; set; }

        public List<Environment> Environments { get; set; }

        [JsonProperty(PropertyName = "Items")]
        public List<Deployment> Deploys { get; set; }
    }
}