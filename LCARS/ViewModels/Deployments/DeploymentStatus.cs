using System.Collections.Generic;

namespace LCARS.ViewModels.Deployments
{
    public class DeploymentStatus
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<Deployment> Deploys { get; set; }
    }
}