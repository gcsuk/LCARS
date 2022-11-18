using LCARS.Configuration.Models;
using LCARS.Octopus.Responses;

namespace LCARS.Octopus
{
    public class MockOctopusService : IOctopusService
    {
        public async Task<IEnumerable<ProjectDeployments>> GetDeployments(OctopusSettings settings)
        {
            var deployments = new List<ProjectDeployments>();

            var environments = new List<string>
            {
                "INT", "STG", "UAT", "PROD"
            };

            var projects = new List<string>
            {
                "Project 1", "Project 2"
            };

            var tenants = new List<string>
            {
                "Tenant 1", "Tenant 2", "Tenant 3", "Tenant 4", "Tenant 5"
            };

            var random = new Random();

            for (var projectIndex = 0; projectIndex < projects.Count; projectIndex++)
            {
                for (var tenantIndex = 0; tenantIndex < tenants.Count; tenantIndex++)
                {
                    var projectDeployment = new ProjectDeployments
                    {
                        ProjectName = $"{projects[projectIndex]} {tenants[tenantIndex]}",
                    };

                    for (int deploymentIndex = 0; deploymentIndex < environments.Count; deploymentIndex++)
                    {
                        projectDeployment.Deployments.Add(new ProjectDeployments.DeploymentModel
                        {
                            Environment = environments[deploymentIndex],
                            ReleaseVersion = $"{random.Next(1, 4)}.{random.Next(1, 20)}.{random.Next(1, 50)}.0",
                            State = random.Next(0, 2) == 0 ? "Success" : "Failure",
                            HasWarningsOrErrors = random.Next(0, 2) == 0,
                            WebUrl = "http://tempuri.org",
                        });
                    }

                    deployments.Add(projectDeployment);
                }
            }

            return await Task.FromResult(deployments);
        }
    }
}