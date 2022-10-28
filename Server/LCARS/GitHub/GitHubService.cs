using LCARS.Configuration.Models;
using LCARS.GitHub.Responses;

namespace LCARS.GitHub
{
    public class GitHubService : IGitHubService
    {
        private readonly IGitHubClient _gitHubClient;

        public GitHubService(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        public async Task<IEnumerable<Branch>> GetBranches(GitHubSettings settings)
        {
            var branches = new List<Branch>();

            foreach (var repository in settings.Repositories ?? Enumerable.Empty<string>())
            {
                var page = 1;

                while (true)
                {
                    var branchSet = await _gitHubClient.GetBranches(settings.Key, settings.Owner, repository, page);

                    if (!branchSet.Any())
                        break;

                    branches.AddRange(branchSet.Select(b => new Branch
                    {
                        Repository = repository,
                        BranchName = b.Name
                    }));

                    page++;
                }
            }

            return branches;
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequests(GitHubSettings settings, bool includeComments = false)
        {
            var pullRequests = new List<PullRequest>();

            foreach (var repository in settings.Repositories)
            {
                var page = 1;

                while (true)
                {
                    var pulls = await _gitHubClient.GetPullRequests(settings.Key, settings.Owner, repository, page);

                    if (!pulls.Any())
                        break;

                    pullRequests.AddRange(pulls.Select(p => new PullRequest
                    {
                        Repository = repository,
                        Number = p.Number,
                        Title = p.Title,
                        Description = p.Description,
                        State = p.State,
                        CreatedOn = p.CreatedOn,
                        Author = p.User.Name,
                        UpdatedOn = p.UpdatedOn
                    }));

                    page++;
                }
            }

            if (includeComments)
                foreach (var pr in pullRequests)
                {
                    var page = 1;

                    while (true)
                    {
                        var comments = await _gitHubClient.GetPullRequestComments(settings.Key, settings.Owner, pr.Repository, pr.Number, page);

                        if (!comments.Any())
                            break;

                        pr.CommentCount += comments.Count();

                        page++;
                    }
                }

            return pullRequests;
        }
    }
}