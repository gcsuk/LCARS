using System.Collections.Generic;
using System.Linq;
using LCARS.Models;
using LCARS.Services;

namespace LCARS.Domain
{
    public class Builds : IBuilds
    {
        private readonly IRepository _repository;

        public Builds(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Build> GetBuilds(string path)
        {
            return _repository.GetBuilds(path);
        }

        public IEnumerable<Build> GetBuildStatus(IEnumerable<int> buildTypeIds)
        {
            var builds = new List<Build>();

            // Get all running builds
            var buildsRunning = _repository.GetBuildsRunning();

            foreach (var buildTypeId in buildTypeIds)
            {
                var build = new Build { TypeId = buildTypeId };

                if (buildsRunning.ContainsKey(buildTypeId))
                {
                    build.Progress = _repository.GetBuildProgress(buildsRunning.Single(b => b.Key == buildTypeId).Value);
                }
                else
                {
                    var lastBuildStatus = _repository.GetLastBuildStatus(buildTypeId);

                    build.Status = lastBuildStatus.Key;
                    build.Number = lastBuildStatus.Value;
                }

                builds.Add(build);
            }

            return builds;
        }
    }
}