using LCARS.BitBucket.Responses;

namespace LCARS.BitBucket;

public interface IBitBucketService
{
    Task<IEnumerable<Branch>> GetBranches();
    Task<IEnumerable<PullRequest>> GetPullRequests();
}