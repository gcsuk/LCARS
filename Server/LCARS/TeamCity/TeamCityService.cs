using LCARS.Configuration.Models;
using LCARS.TeamCity.Responses;
using Refit;

namespace LCARS.TeamCity
{
    public class TeamCityService : ITeamCityService
    {
        private readonly ITeamCityClient _teamCityClient;

        public TeamCityService(ITeamCityClient teamCityClient)
        {
            _teamCityClient = teamCityClient;
        }

        public async Task<IEnumerable<Project>> GetProjects(TeamCitySettings settings)
        {
            var response = await _teamCityClient.GetProjects(settings.AccessToken);

            return response.ProjectData.Select(p => new Project
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        public async Task<IEnumerable<Build>> GetBuilds(TeamCitySettings settings)
        {
            if (!settings.Enabled)
                return Enumerable.Empty<Build>();

            var builds = new List<Build>();

            foreach (var buildItem in settings.Builds ?? Enumerable.Empty<TeamCitySettings.TeamCityBuild>())
            {
                var build = (await _teamCityClient.GetBuilds(settings.AccessToken, buildItem.BuildTypeId))?.Build.FirstOrDefault();

                if (build == null)
                    continue;

                try
                {
                    var runningBuild = await _teamCityClient.GetBuildRunning(settings.AccessToken, buildItem.BuildTypeId);

                    builds.Add(new Build
                    {
                        DisplayName = buildItem.DisplayName,
                        BuildTypeId = runningBuild.BuildTypeId,
                        BuildNumber = runningBuild.Number,
                        State = runningBuild.State,
                        Status = runningBuild.Status,
                        Branch = runningBuild.BranchName,
                        PercentageComplete = runningBuild.RunningInfo.PercentageComplete,
                        ElapsedSeconds = runningBuild.RunningInfo.ElapsedSeconds,
                        CurrentStageText = runningBuild.RunningInfo.CurrentStageText,
                        EstimatedTotalSeconds = runningBuild.RunningInfo.EstimatedTotalSeconds,
                        ProbablyHanging = runningBuild.RunningInfo.ProbablyHanging,
                        WebUrl = runningBuild.WebUrl,
                    });
                }
                catch (ApiException ex)
                {
                    if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                        builds.Add(new Build
                        {
                            DisplayName = buildItem.DisplayName,
                            BuildTypeId = build.BuildTypeId,
                            BuildNumber = build.Number,
                            State = build.State,
                            Status = build.Status,
                            Branch = build.BranchName,
                            PercentageComplete = 100,
                            WebUrl = build.WebUrl,
                        });
                    else
                        throw;
                }
            }

            return builds;
        }
    }
}