using LCARS.Configuration;
using LCARS.Configuration.Models;
using LCARS.GitHub.Responses;

namespace LCARS.GitHub
{
    public class GitHubService : IGitHubService
    {
        private readonly ISettingsService _settingsService;
        private readonly IGitHubClient _gitHubClient;

        public GitHubService(ISettingsService settingsService, IGitHubClient gitHubClient)
        {
            _settingsService = settingsService;
            _gitHubClient = gitHubClient;
        }

        public async Task<IEnumerable<GitHubBranchSummary>> GetBranches()
        {
            var settings = await _settingsService.GetGitHubSettings();

            var summary = new List<GitHubBranchSummary>();

            foreach (var repository in settings.Repositories ?? Enumerable.Empty<string>())
            {
                var branches = new GitHubBranchSummary
                {
                    Repository = repository
                };

                var page = 1;

                while (true)
                {
                    var branchSet = await _gitHubClient.GetBranches(settings.Key, settings.Owner, repository, page);

                    if (!branchSet.Any())
                        break;

                    branches.Branches.AddRange(branchSet.Select(b => new GitHubBranchSummary.GitHubBranchModel
                    {
                        Name = b.Name
                    }));

                    page++;
                }

                summary.Add(branches);
            }

            return summary;
        }

        public async Task<IEnumerable<GitHubPullRequest>> GetPullRequests()
        {
            var settings = await _settingsService.GetGitHubSettings();

            var pullRequests = new List<GitHubPullRequest>();

            foreach (var repository in settings.Repositories)
            {
                var page = 1;

                while (true)
                {
                    var pulls = await _gitHubClient.GetPullRequests(settings.Key, settings.Owner, repository, page);

                    if (!pulls.Any())
                        break;

                    pullRequests.AddRange(pulls.Select(p => new GitHubPullRequest
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