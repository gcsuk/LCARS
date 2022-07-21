using LCARS.Models.GitHub;
using LCARS.Services.ApiClients;

namespace LCARS.Services
{
    public class TeamCityService :  ITeamCityService
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

            return response.Project.Select(p => new Project
            {
                Id = p.Id,
                Name  = p.Name
            });
        }
    }
}