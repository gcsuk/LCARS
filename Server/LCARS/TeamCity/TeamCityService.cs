using LCARS.TeamCity.Responses;

namespace LCARS.TeamCity
{
    public class TeamCityService : ITeamCityService
    {
        private readonly ITeamCityClient _teamCityClient;
        private readonly string _apiKey;

        public TeamCityService(IConfiguration configuration, ITeamCityClient teamCityClient)
        {
            _teamCityClient = teamCityClient;
            _apiKey = $"Bearer {configuration["TeamCity:AccessToken"]}";
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            var response = await _teamCityClient.GetProjects(_apiKey);

            return response.ProjectData.Select(p => new Project
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        public async Task<IEnumerable<Build>> GetBuildsComplete()
        {
            var response = await _teamCityClient.GetBuildsComplete(_apiKey);

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

        public async Task<IEnumerable<Build>> GetBuildsRunning()
        {
            var response = await _teamCityClient.GetBuildsRunning(_apiKey);

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