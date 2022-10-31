using LCARS.Services;

namespace LCARS.Data;

public class BranchService
{
    private readonly IApiClient _apiClient;

    public BranchService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<Branch>> GetGitHubBranchesAsync()
    {
        var branches = await _apiClient.GetGitHubBranches();

        return branches;
    }

    public async Task<IEnumerable<Branch>> GetBitBucketBranchesAsync()
    {
        var branches = await _apiClient.GetBitBucketBranches();

        return branches;
    }
}