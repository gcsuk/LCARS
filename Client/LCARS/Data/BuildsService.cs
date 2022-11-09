using LCARS.Services;

namespace LCARS.Data
{
    public class BuildsService
    {
        private readonly IApiClient _apiClient;

        public BuildsService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<Build>> GetBuildsAsync()
        {
            var builds = await _apiClient.GetTeamCityBuilds();

            return builds;
        }
    }
}