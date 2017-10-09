using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCARS.Models.Builds;
using LCARS.Repositories;
using Refit;

namespace LCARS.Services
{
    public class BuildsService : IBuildsService
    {
        private Settings _settings;
        private readonly IRepository<Settings> _repository;

        public BuildsService(IRepository<Settings> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Build>> GetBuilds(string buildTypeId = "")
        {
            await GetSettings();

            var buildsClient = RestService.For<IBuildsClient>(_settings.ServerUrl);

            var buildTypeIds = new List<string>();

            if (string.IsNullOrEmpty(buildTypeId))
                buildTypeIds.AddRange(_settings.ProjectIds.Split(","));
            else
                buildTypeIds.Add(buildTypeId);

            var builds = new List<Build>();

            foreach (var projectId in buildTypeIds)
            {
                var project = await buildsClient.GetBuildsByType(projectId, 1);

                if (project.Build == null || !project.Build.Any())
                    continue;

                var build = project.Build.First();

                var buildType = await buildsClient.GetBuildTypes(build.BuildTypeId);

                if (buildType != null)
                    build.BuildTypeName = buildType.Name;

                var buildsRunning = await buildsClient.GetBuildsRunning(build.BuildTypeId);

                if (buildsRunning.Build != null && buildsRunning.Build.Any())
                {
                    var status = buildsRunning.Build.First();

                    build.Status = status.Status;
                    build.State = status.State;
                    build.StatusText = status.StatusText;
                    build.PercentageComplete = status.PercentageComplete;
                }

                builds.Add(build);
            }

            return builds;
        }

        public async Task<Build> GetBuild(int id)
        {
            await GetSettings();

            var buildsClient = RestService.For<IBuildsClient>(_settings.ServerUrl);

            var build = await buildsClient.GetBuildDetails(id);

            var buildsRunning = await buildsClient.GetBuildsRunning(build.BuildTypeId);

            if (buildsRunning.Build != null && buildsRunning.Build.Any())
            {
                var status = buildsRunning.Build.First();

                build.Status = status.Status;
                build.State = status.State;
                build.StatusText = status.StatusText;
                build.PercentageComplete = status.PercentageComplete;
            }

            return build;
        }

        public async Task<Settings> GetSettings()
        {
            if (_settings != null)
                return _settings;

            var buildsSettings = (await _repository.GetAll()).SingleOrDefault();

            _settings = buildsSettings ?? throw new InvalidOperationException("Invalid Builds settings");

            return buildsSettings;
        }

        public async Task UpdateSettings(Settings settings)
        {
            await _repository.Update(settings);

            // Force refresh of settings data on next API call
            _settings = null;
        }
    }
}