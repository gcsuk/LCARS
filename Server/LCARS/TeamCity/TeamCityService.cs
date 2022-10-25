using LCARS.Configuration.Models;
using LCARS.TeamCity.Responses;

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

        public async Task<IEnumerable<Build>> GetBuildsComplete(TeamCitySettings settings)
        {
            var response = await _teamCityClient.GetBuildsComplete(settings.AccessToken);

            return response.Build.Select(b => new Build
            {
                Id = b.Id,
                Number = b.Number,
                State = b.State,
                Status = b.Status,
                Branch = b.BranchName,
                PercentageComplete = 100
            });
        }

        public async Task<IEnumerable<Build>> GetBuildsRunning(TeamCitySettings settings)
        {
            var response = await _teamCityClient.GetBuildsRunning(settings.AccessToken);

            return response.Build.Select(b => new Build
            {
                Id = b.Id,
                Number = b.Number,
                State = b.State,
                Status = b.Status,
                Branch = b.BranchName,
                PercentageComplete = b.PercentageComplete
            });
        }
    }
}