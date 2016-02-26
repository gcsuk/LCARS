using System.Collections.Generic;
using System.Linq;
using LCARS.ViewModels.Builds;

namespace LCARS.Domain
{
    public class Builds : IBuilds
    {
        private readonly Services.IBuilds _service;
        private readonly Repository.IRepository<Models.Builds.BuildProject> _settingsRepository;

        public Builds(Services.IBuilds service, Repository.IRepository<Models.Builds.BuildProject> settingsRepository)
        {
            _service = service;
            _settingsRepository = settingsRepository;
        }

        public IEnumerable<BuildProject> GetBuilds(string path)
        {
            return _settingsRepository.GetList(path).Select(bp => new BuildProject
            {
                Id = bp.Id,
                Name = bp.Name,
                Builds = bp.Builds.Select(b => new Build
                {
                    Name = b.Name,
                    TypeId = b.TypeId
                })
            });
        }

        public IEnumerable<Build> GetBuildStatus(IEnumerable<string> buildTypeIds)
        {
            var builds = new List<Build>();

            // Get all running builds
            var buildsRunning = _service.GetBuildsRunning();

            foreach (var buildTypeId in buildTypeIds)
            {
                var build = new Build { TypeId = buildTypeId };

                if (buildsRunning.ContainsKey(buildTypeId))
                {
                    var progress = _service.GetBuildProgress(buildsRunning.Single(b => b.Key == buildTypeId).Value);

                    build.Progress = new BuildProgress
                    {
                        Status = progress.Status,
                        Percentage = progress.Percentage
                    };
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

        public bool UpdateBuild(string filePath, Models.Builds.BuildProject build)
        {
            var builds = _settingsRepository.GetList(filePath).ToList();

            var selectedBuild = builds.SingleOrDefault(b => b.Id == build.Id);

            if (selectedBuild == null) // New item
            {
                builds.Add(new Models.Builds.BuildProject
                {
                    Id = selectedBuild.Id,
                    Name = selectedBuild.Name,
                    Builds = selectedBuild.Builds
                });
            }
            else // Updated item
            {
                selectedBuild.Name = build.Name;
                selectedBuild.Builds = build.Builds;
            }

            _settingsRepository.UpdateList(filePath, builds);

            return selectedBuild == null;
        }
    }
}