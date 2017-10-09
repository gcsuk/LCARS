using System;
using System.Threading.Tasks;
using LCARS.Models.Deployments;
using System.Linq;
using LCARS.Repositories;
using Refit;
using System.Collections.Generic;

namespace LCARS.Services
{
    public class DeploymentsService : IDeploymentsService
    {
        private Settings _settings;
        private readonly IRepository<Settings> _repository;

        public DeploymentsService(IRepository<Settings> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Deployment>> Get()
        {
            await GetSettings();

            var deploymentsClient = RestService.For<IDeploymentsClient>(_settings.ServerUrl);

            var deployments = await deploymentsClient.GetDeployments(_settings.ServerKey);

            deployments.Deploys.ToList().ForEach(d =>
            {
                d.Project = deployments.Projects.Single(e => e.Id == d.ProjectId).Name;
                d.Environment = deployments.Environments.Single(e => e.Id == d.EnvironmentId).Name;
                d.ProjectGroupId = deployments.Projects.Single(g => g.Id == d.ProjectId).ProjectGroupId;
                d.ProjectGroup = deployments.ProjectGroups.Single(pg => pg.Id == deployments.Projects.Single(g => g.Id == d.ProjectId).ProjectGroupId).Name;
            });

            return deployments.Deploys;
        }

        public async Task<Settings> GetSettings()
        {
            if (_settings != null)
                return _settings;

            var deploymentsSettings = (await _repository.GetAll()).SingleOrDefault();

            _settings = deploymentsSettings ?? throw new InvalidOperationException("Invalid Deployments settings");

            return deploymentsSettings;
        }

        public async Task UpdateSettings(Settings settings)
        {
            await _repository.Update(settings);

            // Force refresh of settings data on next API call
            _settings = null;
        }
    }
}