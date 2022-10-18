using LCARS.GitHub.Responses;

namespace LCARS.GitHub;

public class MockGitHubService : IGitHubService
{
    public async Task<IEnumerable<Branch>> GetBranches() => await Task.FromResult(new List<Branch>
    {
        new Branch { BranchName = "branch-1" },
        new Branch { BranchName = "branch-2" },
        new Branch { BranchName = "branch-3" },
        new Branch { BranchName = "branch-4" },
        new Branch { BranchName = "branch-5" },
        new Branch { BranchName = "branch-6" },
        new Branch { BranchName = "branch-7" },
        new Branch { BranchName = "branch-8" },
        new Branch { BranchName = "branch-9" },
        new Branch { BranchName = "branch-10" }
    });

    public async Task<IEnumerable<PullRequest>> GetPullRequests(bool includeComments = false) => await Task.FromResult(new List<PullRequest>
    {
        new PullRequest
        {
            Number = 1,
            Title = "Some PR Title",
            CreatedOn = "2022-01-01",
            UpdatedOn = "2022-01-02",
            Author = "User1",
            CommentCount = 0
        },
        new PullRequest
        {
            Number = 1,
            Title = "Some PR Title",
            CreatedOn = "2022-01-01",
            UpdatedOn = "2022-01-02",
            Author = "User1",
            CommentCount = 2
        }
    });
}