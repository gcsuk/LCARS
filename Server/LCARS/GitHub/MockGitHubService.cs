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
        new() {
            Repository = "my-repo",
            State = "OPEN",
            Number = 1,
            Title = "Some PR Title",
            Description = "Some description",
            CreatedOn = new DateTime(2023, 1, 1),
            UpdatedOn = new DateTime(2023, 1, 2),
            Author = "User1",
            CommentCount = 0
        },
        new() {
            Repository = "my-repo",
            State = "OPEN",
            Number = 1,
            Title = "Another PR Title",
            Description = "Another description",
            CreatedOn = new DateTime(2023, 2, 14),
            UpdatedOn = new DateTime(2023, 2, 15),
            Author = "User1",
            CommentCount = 2
        },
        new() {
            Repository = "my-repo",
            State = "OPEN",
            Number = 1,
            Title = "Yet Another PR Title",
            Description = "Another description",
            CreatedOn = new DateTime(2023, 6, 30),
            UpdatedOn = new DateTime(2023, 6, 30),
            Author = "User2",
            CommentCount = 0
        },
        new() {
            Repository = "my-repo",
            State = "OPEN",
            Number = 1,
            Title = "Fourth PR Title",
            Description = "Another description",
            CreatedOn = new DateTime(2023, 11, 4),
            UpdatedOn = new DateTime(2023, 11, 4),
            Author = "User3",
            CommentCount = 8
        }
    });
}