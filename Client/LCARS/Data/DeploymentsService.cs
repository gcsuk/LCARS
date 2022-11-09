using LCARS.Services;

namespace LCARS.Data
{
    public class DeploymentsService
    {
        private readonly IApiClient _apiClient;
        private readonly SettingsService _settingsService;

        public DeploymentsService(IApiClient apiClient, SettingsService settingsService)
        {
            _apiClient = apiClient;
            _settingsService = settingsService;
        }

        public async Task<IEnumerable<ProjectDeployment>> GetDeploymentsAsync()
        {
            var deployments = await _apiClient.GetOctopusDeployments();

            return deployments;
        }
    }
}