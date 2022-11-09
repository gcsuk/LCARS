using LCARS.Services;

namespace LCARS.Data;

public class PullRequestService
{
    private readonly SettingsService _settingsService;
    private readonly IApiClient _apiClient;

    public PullRequestService(SettingsService settingsService, IApiClient apiClient)
    {
        _settingsService = settingsService;
        _apiClient = apiClient;
    }

    public async Task<PullRequestSummary> GetGitHubPullRequestsAsync()
    {
        var settings = await _settingsService.GetSettings();

        var summary = new PullRequestSummary
        {
            Threshold = settings.GitHubSettings.PullRequestThreshold,
            PullRequests = await _apiClient.GetGitHubPullRequests()
        };

        return summary;
    }

    public async Task<PullRequestSummary> GetBitBucketPullRequestsAsync()
    {
        var settings = await _settingsService.GetSettings();

        var summary = new PullRequestSummary
        {
            Threshold = settings.BitBucketSettings.PullRequestThreshold,
            PullRequests = await _apiClient.GetBitBucketPullRequests()
        };

        return summary;
    }
}