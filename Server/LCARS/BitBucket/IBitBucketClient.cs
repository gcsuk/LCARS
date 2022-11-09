using LCARS.BitBucket.Models;
using Refit;

namespace LCARS.BitBucket;

public interface IBitBucketClient
{
    [Get("/repositories/{owner}?pagelen={pageLength}&page={pageNumber}")]
    Task<Repository> GetRepositories([Header("Authorization")] string token, string owner, int pageLength, int pageNumber);

    [Get("/repositories/{owner}/{repository}/refs/branches?pagelen={pageLength}&page={pageNumber}")]
    Task<Branches> GetBranches([Header("Authorization")] string token, string owner, string repository, int pageLength, int pageNumber);

    [Get("/repositories/{owner}/{repository}/pullrequests?pagelen={pageLength}&page={pageNumber}")]
    Task<PullRequests> GetPullRequests([Header("Authorization")] string token, string owner, string repository, int pageLength, int pageNumber);
}