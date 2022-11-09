using LCARS.Services;

namespace LCARS.Data;

public class BranchService
{
    private readonly SettingsService _settingsService;
    private readonly IApiClient _apiClient;

    public BranchService(SettingsService settingsService, IApiClient apiClient)
    {
        _settingsService = settingsService;
        _apiClient = apiClient;
    }

    public async Task<BranchSummary> GetGitHubBranchesAsync()
    {
        var settings = await _settingsService.GetSettings();

        var summary = new BranchSummary
        {
            Threshold = settings.GitHubSettings.BranchThreshold,
            Repositories = await _apiClient.GetGitHubBranches()
        };

        return summary;
    }

    public async Task<BranchSummary> GetBitBucketBranchesAsync()
    {
        var settings = await _settingsService.GetSettings();

        var summary = new BranchSummary
        {
            Threshold = settings.BitBucketSettings.BranchThreshold,
            Repositories = await _apiClient.GetBitBucketBranches()
        };

        return summary;
    }
}