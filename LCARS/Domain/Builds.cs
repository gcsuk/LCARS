using System.Collections.Generic;
using System.Linq;
using LCARS.Models.Builds;

namespace LCARS.Domain
{
    public class Builds : IBuilds
    {
        private readonly Services.IBuilds _service;
        private readonly Repository.IRepository<BuildProject> _settingsRepository;

        public Builds(Services.IBuilds service, Repository.IRepository<BuildProject> settingsRepository)
        {
            _service = service;
            _settingsRepository = settingsRepository;
        }

        public IEnumerable<BuildProject> GetBuilds(string path)
        {
            return _settingsRepository.GetList(path);
        }

        public IEnumerable<Build> GetBuildStatus(IEnumerable<int> buildTypeIds)
        {
            var builds = new List<Build>();

            // Get all running builds
            var buildsRunning = _service.GetBuildsRunning();

            foreach (var buildTypeId in buildTypeIds)
            {
                var build = new Build { TypeId = buildTypeId };

                if (buildsRunning.ContainsKey(buildTypeId))
                {
                    build.Progress = _service.GetBuildProgress(buildsRunning.Single(b => b.Key == buildTypeId).Value);
                }
                else
                {
                    var lastBuildStatus = _service.GetLastBuildStatus(buildTypeId);

                    build.Status = lastBuildStatus.Key;
                    build.Number = lastBuildStatus.Value;
                }

                builds.Add(build);
            }

            return builds;
        }
    }
}