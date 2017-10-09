using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LCARS.Repositories;
using LCARS.Models.GitHub;
using Refit;

namespace LCARS.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IRepository<Settings> _gitHubRepository;
        private Settings _settings;

        public GitHubService(IRepository<Settings> gitHubRepository)
        {
            _gitHubRepository = gitHubRepository;
        }

        public async Task<IEnumerable<Branch>> GetBranches(string repository = null)
        {
            var client = await GetClient<Branch>(repository);

            return await GetData(repository, "branches", client.GetBranches);
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequests(string repository = null, bool includeComments = false)
        {
            var client = await GetClient<PullRequest>(repository);

            var pullRequests = await GetData(repository, "pulls", client.GetPullRequests);

            if (includeComments)
                foreach (var pr in pullRequests)
                {
                    pr.Comments = await GetComments(repository, pr.Number);
                }

            return pullRequests;
        }

        public async Task<IEnumerable<Comment>> GetComments(string repository = null, int pullRequestNumber = 0)
        {
            if (pullRequestNumber <= 0)
            {
                throw new ArgumentException("Invalid pull request number");
            }

            var client = await GetClient<Comment>(repository);

            return await GetData(repository, pullRequestNumber.ToString(), client.GetComments);
        }

        public async Task<Settings> GetSettings()
        {
            if (_settings != null)
                return _settings;

            var gitHubSettings = (await _gitHubRepository.GetAll()).SingleOrDefault();

            _settings = gitHubSettings ?? throw new InvalidOperationException("Invalid GitHub settings");

            return gitHubSettings;
        }

        public async Task UpdateSettings(Settings settings)
        {
            await _gitHubRepository.Update(settings);

            // Force refresh of settings data on next API call
            _settings = null;
        }

        private async Task<IEnumerable<T>> GetData<T>(string repository, string type, Func<string, string, string, int, Task<IEnumerable<T>>> api) where T : class
        {
            await GetSettings();

            repository = ParseRepository(repository, _settings);

            var token = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_settings.Username}:{_settings.Password}"));

            var items = new List<T>();
            var hasRecords = true;
            var pageNumber = 1;

            while (hasRecords)
            {
                var newItems = await api(repository, token, type, pageNumber);

                if (newItems.Any())
                {
                    items.AddRange(newItems);

                    pageNumber++;
                }
                else
                {
                    hasRecords = false;
                }
            }

            return items;
        }

        private static string ParseRepository(string repository, Settings settings)
        {
            if (!string.IsNullOrWhiteSpace(repository))
            {
                return repository;
            }

            if (!settings.Repositories.Any())
            {
                throw new InvalidOperationException("No repositories are configured in the settings database, and no repository was supplied as argument.");
            }

            return settings.Repositories.First().Replace("\"", "");
        }

        private async Task<IGitHubClient<T>> GetClient<T>(string repository)
        {
            await GetSettings();

            return RestService.For<IGitHubClient<T>>(_settings.BaseUrl.Replace("REPOSITORY", repository).Replace("OWNER", _settings.Owner));
        }
    }
}