using LCARS.GitHub.Responses;

namespace LCARS.GitHub;

public class MockGitHubService : IGitHubService
{
    public async Task<IEnumerable<GitHubBranchSummary>> GetBranches() => await Task.FromResult(new List<GitHubBranchSummary>
    {
        new GitHubBranchSummary
        {
            Repository = "my-repo",
            Branches = new List<GitHubBranchSummary.GitHubBranchModel>
            {
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-1" },
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-2" },
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-3" },
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-4" }
            }
        },
        new GitHubBranchSummary
        {
            Repository = "my-repo-2",
            Branches = new List<GitHubBranchSummary.GitHubBranchModel>
            {
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-1" },
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-2" },
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-3" },
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-4" },
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-5" },
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-6" },
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-7" },
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-8" }
            }
        },
        new GitHubBranchSummary
        {
            Repository = "my-repo-3",
            Branches = new List<GitHubBranchSummary.GitHubBranchModel>
            {
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-1" },
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-2" },
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-3" },
            }
        },
        new GitHubBranchSummary
        {
            Repository = "my-repo-4",
            Branches = new List<GitHubBranchSummary.GitHubBranchModel>
            {
                new GitHubBranchSummary.GitHubBranchModel { Name = "branch-1" }
            }
        },
    });

    public async Task<IEnumerable<GitHubPullRequest>> GetPullRequests() => await Task.FromResult(new List<GitHubPullRequest>
    {
        new GitHubPullRequest
        {
            Repository = "my-repo",
            State = "OPEN",
            Number = 1,
            Title = "Some PR Title",
            Description = "Some description",
            CreatedOn = new DateTime(2022, 1, 1),
            UpdatedOn = new DateTime(2022, 1, 2),
            Author = "User1",
            CommentCount = 0
        },
        new GitHubPullRequest
        {
            Repository = "my-repo",
            State = "OPEN",
            Number = 1,
            Title = "Some PR Title",
            Description = "Another description",
            CreatedOn = new DateTime(2022, 1, 1),
            UpdatedOn = new DateTime(2022, 1, 2),
            Author = "User1",
            CommentCount = 2
        }
    });
}