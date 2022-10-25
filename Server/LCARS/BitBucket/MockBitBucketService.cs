using LCARS.BitBucket.Responses;
using LCARS.Configuration.Models;

namespace LCARS.BitBucket;

public class MockBitBucketService : IBitBucketService
{
    public async Task<IEnumerable<Branch>> GetBranches(BitBucketSettings settings) => await Task.FromResult(new List<Branch>
    {
        new Branch { Repository = "my-repo", BranchName = "branch-1", DateCreated = DateTime.Now.AddDays(-1), User = "User 1" },
        new Branch { Repository = "my-repo", BranchName = "branch-2", DateCreated = DateTime.Now.AddDays(-2), User = "User 1" },
        new Branch { Repository = "my-repo", BranchName = "branch-3", DateCreated = DateTime.Now.AddDays(-3), User = "User 2" },
        new Branch { Repository = "my-repo", BranchName = "branch-4", DateCreated = DateTime.Now.AddDays(-4), User = "User 2" },
        new Branch { Repository = "my-repo", BranchName = "branch-5", DateCreated = DateTime.Now.AddDays(-5), User = "User 2" },
        new Branch { Repository = "my-repo", BranchName = "branch-6", DateCreated = DateTime.Now.AddDays(-6), User = "User 3" },
        new Branch { Repository = "my-repo", BranchName = "branch-7", DateCreated = DateTime.Now.AddDays(-7), User = "User 4" },
        new Branch { Repository = "my-repo", BranchName = "branch-8", DateCreated = DateTime.Now.AddDays(-8), User = "User 4" },
        new Branch { Repository = "my-repo", BranchName = "branch-9", DateCreated = DateTime.Now.AddDays(-9), User = "User 4" },
        new Branch { Repository = "my-repo", BranchName = "branch-10", DateCreated = DateTime.Now, User = "User 4" }
    });

    public async Task<IEnumerable<PullRequest>> GetPullRequests(BitBucketSettings settings) => await Task.FromResult(new List<PullRequest>
    {
        new PullRequest
        {
            Repository = "Some Repository",
            Number = 1,
            Title = "Some PR Title",
            Description = "Description!",
            State = "OPEN",
            CreatedOn = "2022-01-01",
            UpdatedOn = "2022-01-02",
            Author = "User1",
            CommentCount = 0
        },
        new PullRequest
        {
            Repository = "Some Repository",
            Number = 1,
            Title = "Some PR Title",
            Description = "Another Description!",
            State = "OPEN",
            CreatedOn = "2022-01-01",
            UpdatedOn = "2022-01-02",
            Author = "User1",
            CommentCount = 2
        }
    });
}