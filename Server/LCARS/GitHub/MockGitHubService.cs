using LCARS.GitHub.Models;

namespace LCARS.GitHub;

public class MockGitHubService : IGitHubService
{
    public async Task<IEnumerable<Branch>> GetBranches() => await Task.FromResult(new List<Branch>
    {
        new Branch { Name = "branch-1" },
        new Branch { Name = "branch-2" },
        new Branch { Name = "branch-3" },
        new Branch { Name = "branch-4" },
        new Branch { Name = "branch-5" },
        new Branch { Name = "branch-6" },
        new Branch { Name = "branch-7" },
        new Branch { Name = "branch-8" },
        new Branch { Name = "branch-9" },
        new Branch { Name = "branch-10" }
    });

    public async Task<IEnumerable<PullRequest>> GetPullRequests(bool includeComments = false) => await Task.FromResult(new List<PullRequest>
    {
        new PullRequest
        {
            Number = 1,
            Title = "Some PR Title",
            CreatedOn = "2022-01-01",
            UpdatedOn = "2022-01-02",
            User = new User
            {
                Name = "User1",
                Avatar = "https://avatar.png"
            },
            Comments = new List<Comment>()
        },
        new PullRequest
        {
            Number = 1,
            Title = "Some PR Title",
            CreatedOn = "2022-01-01",
            UpdatedOn = "2022-01-02",
            User = new User
            {
                Name = "User1",
                Avatar = "https://avatar.png"
            },
            Comments = new List<Comment>
            {
                new Comment
                {
                    DateCreated = DateTime.Now,
                    User = new User
                    {
                        Name = "User2",
                        Avatar = "https://avatar2.png"
                    },
                    Body = "Some comment"
                },
                new Comment
                {
                    DateCreated = DateTime.Now,
                    User = new User
                    {
                        Name = "User2",
                        Avatar = "https://avatar2.png"
                    },
                    Body = "Another comment"
                }
            }
        }
    });
}