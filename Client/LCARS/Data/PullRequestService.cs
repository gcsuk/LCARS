using LCARS.Services;

namespace LCARS.Data
{
    public class PullRequestService
    {
        private readonly IApiClient _apiClient;

        public PullRequestService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequestsAsync()
        {
            var pullRequests = await _apiClient.GetGitHubPullRequests();



            return pullRequests;
        }
    }
}