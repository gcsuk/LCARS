using LCARS.Services;

namespace LCARS.Data;

public class PullRequestService
{
    private readonly IApiClient _apiClient;

    public PullRequestService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<PullRequest>> GetGitHubPullRequestsAsync()
    {
        var pullRequests = await _apiClient.GetGitHubPullRequests();

        return pullRequests;
    }

    public async Task<IEnumerable<PullRequest>> GetBitBucketPullRequestsAsync()
    {
        var pullRequests = await _apiClient.GetBitBucketPullRequests();

        return pullRequests;
    }
}