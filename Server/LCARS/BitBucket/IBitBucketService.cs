using LCARS.BitBucket.Responses;

namespace LCARS.BitBucket;

public interface IBitBucketService
{
    Task<IEnumerable<BitBucketBranchSummary>> GetBranches();
    Task<IEnumerable<BitBucketPullRequest>> GetPullRequests();
}