using LCARS.Configuration.Models;
using LCARS.Octopus.Responses;

namespace LCARS.Octopus
{
    public class OctopusService : IOctopusService
    {
        private readonly IOctopusClient _octopusClient;

        public OctopusService(IOctopusClient octopusClient)
        {
            _octopusClient= octopusClient;
        }

        public async Task<IEnumerable<ProjectDeployments>> GetDeployments(OctopusSettings settings)
        {
            var summary = new List<ProjectDeployments>();

            var dashboard = await _octopusClient.GetDashboard(settings.ApiKey);

            var environments = dashboard.Environments;
            var projects = dashboard.Projects;

            foreach (var project in projects)
            {
                var projectDeployments = new List<ProjectDeployments.DeploymentModel>();

                foreach (var item in dashboard.Items.Where(i => i.ProjectId == project.Id))
                {
                    var environment = environments.SingleOrDefault(e => e.Id == item.EnvironmentId);

                    if (environment == null)
                        continue;

                    projectDeployments.Add(new ProjectDeployments.DeploymentModel
                    {
                        Environment = environment.Name,
                        ReleaseVersion = item.ReleaseVersion,
                        HasWarningsOrErrors = item.HasWarningsOrErrors,
                        State = item.State
                    });
                }

                summary.Add(new ProjectDeployments
                {
                    ProjectName = project.Name,
                    Deployments = projectDeployments,
                });
            }

            return summary;
        }
    }
}