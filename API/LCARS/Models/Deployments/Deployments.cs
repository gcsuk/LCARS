using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonPropertyName("items")]
        public IEnumerable<Deployment> Deploys { get; set; }
    }
}