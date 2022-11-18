using LCARS.Configuration.Models;
using LCARS.Octopus.Responses;

namespace LCARS.Octopus
{
    public class OctopusService : IOctopusService
    {
        private readonly IOctopusClient _octopusClient;
        private readonly IConfiguration _configuration;
        public OctopusService(IOctopusClient octopusClient, IConfiguration configuration)
        {
            _octopusClient = octopusClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ProjectDeployments>> GetDeployments(OctopusSettings settings)
        {
            var summary = new List<ProjectDeployments>();

            var dashboard = await _octopusClient.GetDashboard(settings.ApiKey);

            var environments = dashboard.Environments;
            var projects = dashboard.Projects;
            var tenants = dashboard.Tenants;
            var octopusBaseUrl = _configuration["Octopus:BaseUrl"];

            foreach (var project in projects)
            {
                foreach (var tenant in tenants)
                {
                    var projectDeployments = new List<ProjectDeployments.DeploymentModel>();

                    foreach (var item in dashboard.Items.Where(i => i.ProjectId == project.Id && i.TenantId == tenant.Id))
                    {
                        var environment = environments.SingleOrDefault(e => e.Id == item.EnvironmentId);

                        if (environment == null)
                            continue;

                        projectDeployments.Add(new ProjectDeployments.DeploymentModel
                        {
                            Environment = environment.Name,
                            ReleaseVersion = item.ReleaseVersion,
                            HasWarningsOrErrors = item.HasWarningsOrErrors,
                            State = item.State,
                            WebUrl = $"{octopusBaseUrl}/app#/projects/{project.Slug}/deployments/releases/{item.ReleaseVersion}/deployments/{item.Id}"
                        });
                    }

                    if (projectDeployments.Any())
                        summary.Add(new ProjectDeployments
                        {
                            ProjectName = $"{project.Name} {tenant.Name}",
                            Deployments = projectDeployments,
                        });
                }
            }

            return summary;
        }
    }
}