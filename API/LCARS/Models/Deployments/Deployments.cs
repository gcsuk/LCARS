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

        public IEnumerable<ProjectGroup> ProjectGroups { get; set; }

        public IEnumerable<Project> Projects { get; set; }

        public IEnumerable<Environment> Environments { get; set; }

        [JsonProperty(PropertyName = "Items")]
        public IEnumerable<Deployment> Deploys { get; set; }
    }
}