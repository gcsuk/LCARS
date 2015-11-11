using System.Collections.Generic;
using System.Linq;
using LCARS.ViewModels.Deployments;

namespace LCARS.Domain
{
    public class Deployments : IDeployments
    {
        private readonly Services.IDeployments _service;
        private readonly Repository.IRepository<Models.Deployments.Environment> _settingsRepository;

        public Deployments(Services.IDeployments service, Repository.IRepository<Models.Deployments.Environment> settingsRepository)
        {
            _service = service;
            _settingsRepository = settingsRepository;
        }

        public IEnumerable<Deployment> Get()
        {
            var deployments = _service.Get();

            deployments.Deploys.ForEach(d =>
            {
                d.Project = deployments.Projects.Single(e => e.Id == d.ProjectId).Name;
                d.Environment = deployments.Environments.Single(e => e.Id == d.EnvironmentId).Name;
            });

            return deployments.Deploys.Select(deployment => new Deployment
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

        public IEnumerable<Environment> SetEnvironmentOrder(IEnumerable<Environment> environments, string preferencesFilePath)
        {
            var preferences = _settingsRepository.GetList(preferencesFilePath);

            foreach (var environment in environments.ToList())
            {
                var preference = preferences.SingleOrDefault(p => p.Id == environment.Id);

                if (preference != null)
                    environment.OrderId = preference.OrderId;
            }

            return environments.OrderBy(e => e.OrderId);
        }
    }
}