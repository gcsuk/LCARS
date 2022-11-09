using LCARS.GitHub.Responses;

namespace LCARS.GitHub;

public interface IGitHubService
{
    Task<IEnumerable<GitHubBranchSummary>> GetBranches();
    Task<IEnumerable<GitHubPullRequest>> GetPullRequests();
}