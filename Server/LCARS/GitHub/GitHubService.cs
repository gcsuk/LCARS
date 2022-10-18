using LCARS.GitHub.Responses;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LCARS.GitHub
{
    public class GitHubService : IGitHubService
    {
        private readonly IGitHubClient _gitHubClient;
        private readonly string _apiKey;
        private readonly string _owner;
        private readonly IEnumerable<string> _repositories;

        public GitHubService(IConfiguration configuration, IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
            _apiKey = $"Bearer {configuration["GitHub:Key"]}";
            _owner = configuration["GitHub:Owner"] ?? "";
            _repositories = configuration.GetSection("GitHub:Repositories").Get<List<string>>() ?? Enumerable.Empty<string>();
        }

        public async Task<IEnumerable<Branch>> GetBranches()
        {
            var branches = new List<Branch>();

            foreach (var repository in _repositories)
            {
                var page = 1;

                while (true)
                {
                    var branchSet = await _gitHubClient.GetBranches(_apiKey, _owner, repository, page);

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

        public async Task<IEnumerable<PullRequest>> GetPullRequests(bool includeComments = false)
        {
            var pullRequests = new List<PullRequest>();

            foreach (var repository in _repositories)
            {
                var page = 1;

                while (true)
                {
                    var pulls = await _gitHubClient.GetPullRequests(_apiKey, _owner, repository, page);

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
                        var comments = await _gitHubClient.GetPullRequestComments(_apiKey, _owner, pr.Repository, pr.Number, page);

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