using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using LCARS.Models;
using LCARS.Models.Deployments;
using System.Linq;

namespace LCARS.Services
{
    public class DeploymentsService : IDeploymentsService
    {
        private readonly Settings _settings;

        public DeploymentsService(ISettingsService settingsService)
        {
            _settings = settingsService.GetSettings();
        }

        public async Task<IEnumerable<ViewModels.Deployments.Deployment>> Get()
        {
            var jsonData = "";

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-Octopus-ApiKey", _settings.DeploymentsServerKey);

                    jsonData = await client.GetStringAsync(_settings.DeploymentsServerUrl);
                }
            }
            catch
            {
                // jsonData will be empty
            }

            var deployments = !string.IsNullOrEmpty(jsonData)
                ? JsonConvert.DeserializeObject<Deployments>(jsonData)
                : new Deployments();

            deployments.Deploys.ForEach(d =>
            {
                d.Project = deployments.Projects.Single(e => e.Id == d.ProjectId).Name;
                d.Environment = deployments.Environments.Single(e => e.Id == d.EnvironmentId).Name;
            });

            return deployments.Deploys.Select(deployment => new ViewModels.Deployments.Deployment
            {
                ProjectGroupId = deployments.Projects.Single(g => g.Id == deployment.ProjectId).ProjectGroupId,
                ProjectGroup = deployments.ProjectGroups.Single(pg => pg.Id == deployments.Projects.Single(g => g.Id == deployment.ProjectId).ProjectGroupId).Name,
                ProjectId = deployment.ProjectId,
                Project = deployment.Project,
                EnvironmentId = deployment.EnvironmentId,
                Environment = deployment.Environment,
                CompletedTime = deployment.CompletedTime,
                Duration = deployment.Duration,
                State = deployment.State,
                HasWarningsOrErrors = deployment.HasWarningsOrErrors,
                ReleaseVersion = deployment.ReleaseVersion
            }).ToList();
        }
    }
}