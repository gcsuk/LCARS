using LCARS.Models.GitHub;

namespace LCARS.Services;

public interface IGitHubService
{
    Task<IEnumerable<Branch>> GetBranches();
    Task<IEnumerable<PullRequest>> GetPullRequests(bool includeComments = false);
}