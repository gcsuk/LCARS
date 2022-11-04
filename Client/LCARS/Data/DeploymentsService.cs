using LCARS.Services;

namespace LCARS.Data
{
    public class DeploymentsService
    {
        private readonly IApiClient _apiClient;

        public DeploymentsService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<ProjectDeployment>> GetDeploymentsAsync()
        {
            var deployments = await _apiClient.GetOctopusDeployments();

            return deployments;
        }
    }
}