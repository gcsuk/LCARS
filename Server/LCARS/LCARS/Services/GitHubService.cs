using LCARS.Models.GitHub;
using LCARS.Services.ApiClients;

namespace LCARS.Services
{
    public class GitHubService :  IGitHubService
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
                branches.AddRange(await _gitHubClient.GetData<Branch>(_apiKey, _owner, repository, "branches", 1));

            return branches;
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequests(bool includeComments = false)
        {
            var pullRequests = new List<PullRequest>();

            foreach (var repository in _repositories)
            {
                var pulls = await _gitHubClient.GetData<PullRequest>(_apiKey, _owner, repository, "pulls", 1);

                if (includeComments)
                    foreach (var pr in pulls)
                        pr.Comments = await _gitHubClient.GetData<Comment>(_apiKey, _owner, repository, $"pulls/{pr.Number}/comments", 1);

                pullRequests.AddRange(pulls);
            }

            return pullRequests;
        }
    }
}