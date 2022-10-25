using LCARS.BitBucket.Responses;
using LCARS.Configuration.Models;

namespace LCARS.BitBucket;

public interface IBitBucketService
{
    Task<IEnumerable<Branch>> GetBranches(BitBucketSettings settings);
    Task<IEnumerable<PullRequest>> GetPullRequests(BitBucketSettings settings);
}