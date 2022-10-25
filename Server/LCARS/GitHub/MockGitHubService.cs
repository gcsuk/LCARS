using LCARS.Configuration.Models;
using LCARS.GitHub.Responses;

namespace LCARS.GitHub;

public class MockGitHubService : IGitHubService
{
    public async Task<IEnumerable<Branch>> GetBranches(GitHubSettings settings) => await Task.FromResult(new List<Branch>
    {
        new Branch { Repository = "my-repo", BranchName = "branch-1" },
        new Branch { Repository = "my-repo", BranchName = "branch-2" },
        new Branch { Repository = "my-repo", BranchName = "branch-3" },
        new Branch { Repository = "my-repo", BranchName = "branch-4" },
        new Branch { BranchName = "branch-5" },
        new Branch { Repository = "my-repo", BranchName = "branch-6" },
        new Branch { Repository = "my-repo", BranchName = "branch-7" },
        new Branch { Repository = "my-repo", BranchName = "branch-8" },
        new Branch { Repository = "my-repo", BranchName = "branch-9" },
        new Branch { Repository = "my-repo", BranchName = "branch-10" }
    });

    public async Task<IEnumerable<PullRequest>> GetPullRequests(GitHubSettings settings, bool includeComments = false) => await Task.FromResult(new List<PullRequest>
    {
        new PullRequest
        {
            Repository = "my-repo",
            State = "OPEN",
            Number = 1,
            Title = "Some PR Title",
            Description = "Some description",
            CreatedOn = "2022-01-01",
            UpdatedOn = "2022-01-02",
            Author = "User1",
            CommentCount = 0
        },
        new PullRequest
        {
            Repository = "my-repo",
            State = "OPEN",
            Number = 1,
            Title = "Some PR Title",
            Description = "Another description",
            CreatedOn = "2022-01-01",
            UpdatedOn = "2022-01-02",
            Author = "User1",
            CommentCount = 2
        }
    });
}