using LCARS.Configuration.Models;
using LCARS.GitHub.Responses;

namespace LCARS.GitHub;

public interface IGitHubService
{
    Task<IEnumerable<Branch>> GetBranches(GitHubSettings settings);
    Task<IEnumerable<PullRequest>> GetPullRequests(GitHubSettings settings, bool includeComments = false);
}