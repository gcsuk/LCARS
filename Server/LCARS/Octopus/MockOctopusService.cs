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
                "Project 1", "Project 2", "Project 3", "Project 4", "Project 5"
            };

            var random = new Random();

            for (var i = 0; i < 5; i++)
            {
                var projectDeployment = new ProjectDeployments
                {
                    ProjectName = projects[i]
                };

                for (int j = 0; j < 4; j++)
                {
                    projectDeployment.Deployments.Add(new ProjectDeployments.DeploymentModel
                    {
                        Environment = environments[j],
                        ReleaseVersion = $"{random.Next(1, 4)}.{random.Next(1, 20)}.{random.Next(1, 50)}.0",
                        State = random.Next(0, 2) == 0 ? "Success" : "Failure",
                        HasWarningsOrErrors = random.Next(0, 2) == 0,
                    });
                }

                deployments.Add(projectDeployment);
            }

            return await Task.FromResult(deployments);
        }
    }
}