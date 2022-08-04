using LCARS.GitHub.Models;
using LCARS.Services.ApiClients;

namespace LCARS.GitHub
{
    public class GitHubService : IGitHubService
    {
        private readonly IGitHubClient _gitHubClient;
        private readonly string _apiKey;
        private readonly string _owner;
        private readonly List<string> _repositories;

        public GitHubService(IConfiguration configuration, IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
            _apiKey = $"Bearer {configuration["GitHub:Key"]}";
            _owner = configuration["GitHub:Owner"];
            _repositories = configuration.GetSection("GitHub:Repositories").Get<List<string>>();
        }

        public async Task<IEnumerable<Branch>> GetBranches()
        {
            var branches = new List<Branch>();

            foreach (var repository in _repositories)
                branches.AddRange(await _gitHubClient.GetBranches(_apiKey, _owner, repository, 1));

            return branches;
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequests(bool includeComments = false)
        {
            var pullRequests = new List<PullRequest>();

            foreach (var repository in _repositories)
            {
                var pulls = await _gitHubClient.GetPullRequests(_apiKey, _owner, repository, 1);

                if (includeComments)
                    foreach (var pr in pulls)
                        pr.Comments = await _gitHubClient.GetPullRequestComments(_apiKey, _owner, repository, pr.Number, 1);

                pullRequests.AddRange(pulls);
            }

            return pullRequests;
        }
    }
}