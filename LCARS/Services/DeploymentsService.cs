using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using LCARS.Models.Deployments;
using System.Linq;
using LCARS.Repositories;

namespace LCARS.Services
{
    public class DeploymentsService : IDeploymentsService
    {
        private Settings _settings;
        private readonly IDeploymentsRepository _repository;

        public DeploymentsService(IDeploymentsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Deployments> Get()
        {
            await GetSettings();

            string jsonData;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Octopus-ApiKey", _settings.ServerKey);

                jsonData = await client.GetStringAsync(_settings.ServerUrl);
            }

            Deployments deployments;

            try
            {
                deployments = !string.IsNullOrEmpty(jsonData)
                    ? JsonConvert.DeserializeObject<Deployments>(jsonData)
                    : new Deployments();
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("Invalid response from deployment server.", ex);
            }

            deployments.Deploys.ForEach(d =>
            {
                d.Project = deployments.Projects.Single(e => e.Id == d.ProjectId).Name;
                d.Environment = deployments.Environments.Single(e => e.Id == d.EnvironmentId).Name;
            });

            return deployments;
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