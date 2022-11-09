using LCARS.GitHub.Models;
using Refit;

namespace LCARS.GitHub;

[Headers(new[] { "User-Agent: LCARS" })]
public interface IGitHubClient
{
    [Get("/repos/{owner}/{repository}/branches?per_page=100&page={pageNumber}")]
    Task<IEnumerable<Branch>> GetBranches([Header("Authorization")] string token, string owner, string repository, int pageNumber);

    [Get("/repos/{owner}/{repository}/pulls?per_page=100&page={pageNumber}")]
    Task<IEnumerable<PullRequest>> GetPullRequests([Header("Authorization")] string token, string owner, string repository, int pageNumber);

    [Get("/repos/{owner}/{repository}/pulls/{number}/comments?per_page=100&page={pageNumber}")]
    Task<IEnumerable<Comment>> GetPullRequestComments([Header("Authorization")] string token, string owner, string repository, int number, int pageNumber);
}