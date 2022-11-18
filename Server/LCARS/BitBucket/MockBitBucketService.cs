using LCARS.BitBucket.Responses;

namespace LCARS.BitBucket;

public class MockBitBucketService : IBitBucketService
{
    public async Task<IEnumerable<BitBucketBranchSummary>> GetBranches() => await Task.FromResult(new List<BitBucketBranchSummary>
    {
        new BitBucketBranchSummary { Repository = "my-repo", Branches = new List<BitBucketBranchSummary.BitBucketBranchModel> { new BitBucketBranchSummary.BitBucketBranchModel { Name = "branch-1", DateCreated = DateTime.Now.AddDays(-1), User = "User 1" } } },
        new BitBucketBranchSummary { Repository = "my-repo", Branches = new List<BitBucketBranchSummary.BitBucketBranchModel> { new BitBucketBranchSummary.BitBucketBranchModel { Name = "branch-2", DateCreated = DateTime.Now.AddDays(-2), User = "User 1" } } },
        new BitBucketBranchSummary { Repository = "my-repo", Branches = new List<BitBucketBranchSummary.BitBucketBranchModel> { new BitBucketBranchSummary.BitBucketBranchModel { Name = "branch-3", DateCreated = DateTime.Now.AddDays(-3), User = "User 2" } } },
        new BitBucketBranchSummary { Repository = "my-repo", Branches = new List<BitBucketBranchSummary.BitBucketBranchModel> { new BitBucketBranchSummary.BitBucketBranchModel { Name = "branch-4", DateCreated = DateTime.Now.AddDays(-4), User = "User 2" } } },
        new BitBucketBranchSummary { Repository = "my-repo", Branches = new List<BitBucketBranchSummary.BitBucketBranchModel> { new BitBucketBranchSummary.BitBucketBranchModel { Name = "branch-5", DateCreated = DateTime.Now.AddDays(-5), User = "User 2" } } },
        new BitBucketBranchSummary { Repository = "my-repo", Branches = new List<BitBucketBranchSummary.BitBucketBranchModel> { new BitBucketBranchSummary.BitBucketBranchModel { Name = "branch-6", DateCreated = DateTime.Now.AddDays(-6), User = "User 3" } } },
        new BitBucketBranchSummary { Repository = "my-repo", Branches = new List<BitBucketBranchSummary.BitBucketBranchModel> { new BitBucketBranchSummary.BitBucketBranchModel { Name = "branch-7", DateCreated = DateTime.Now.AddDays(-7), User = "User 4" } } },
        new BitBucketBranchSummary { Repository = "my-repo", Branches = new List<BitBucketBranchSummary.BitBucketBranchModel> { new BitBucketBranchSummary.BitBucketBranchModel { Name = "branch-8", DateCreated = DateTime.Now.AddDays(-8), User = "User 4" } } },
        new BitBucketBranchSummary { Repository = "my-repo", Branches = new List<BitBucketBranchSummary.BitBucketBranchModel> { new BitBucketBranchSummary.BitBucketBranchModel { Name = "branch-9", DateCreated = DateTime.Now.AddDays(-9), User = "User 4" } } },
        new BitBucketBranchSummary { Repository = "my-repo", Branches = new List<BitBucketBranchSummary.BitBucketBranchModel> { new BitBucketBranchSummary.BitBucketBranchModel { Name = "branch-10", DateCreated = DateTime.Now, User = "User 4" } } }
    });

    public async Task<IEnumerable<BitBucketPullRequest>> GetPullRequests() => await Task.FromResult(new List<BitBucketPullRequest>
    {
        new BitBucketPullRequest
        {
            Repository = "Some Repository",
            Number = 1,
            Title = "Some PR Title",
            Description = "Description!",
            State = "OPEN",
            CreatedOn = new DateTime(2022, 1, 1),
            UpdatedOn = new DateTime(2022, 1, 2),
            Author = "User1",
            CommentCount = 0
        },
        new BitBucketPullRequest
        {
            Repository = "Some Repository",
            Number = 1,
            Title = "Some PR Title",
            Description = "Another Description!",
            State = "OPEN",
            CreatedOn = new DateTime(2022, 1, 1),
            UpdatedOn = new DateTime(2022, 1, 2),
            Author = "User1",
            CommentCount = 2
        }
    });
}