using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LCARS.Repositories;
using LCARS.Models.GitHub;
using Newtonsoft.Json;

namespace LCARS.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IGitHubRepository _gitHubRepository;
        private Settings _settings;

        public GitHubService(IGitHubRepository gitHubRepository)
        {
            _gitHubRepository = gitHubRepository;
        }

        public async Task<IEnumerable<Branch>> GetBranches(string repository = null)
        {
            await GetSettings();

            repository = ParseRepository(repository, _settings);

            var branches = await GetData<Branch>(_settings.BaseUrl.Replace("REPOSITORY", repository).Replace("OWNER", _settings.Owner) + "/branches", repository);
            
            return branches;
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequests(string repository = null, bool includeComments = false)
        {
            await GetSettings();

            repository = ParseRepository(repository, _settings);

            var pullRequests = await GetData<PullRequest>(_settings.BaseUrl.Replace("REPOSITORY", repository).Replace("OWNER", _settings.Owner) + "/pulls", repository);

            if (includeComments)
                foreach (var pr in pullRequests)
                {
                    pr.Comments = await GetComments(repository, pr.Number);
                }
                

            return pullRequests;
        }

        public async Task<IEnumerable<Comment>> GetComments(string repository = null, int pullRequestNumber = 0)
        {
            await GetSettings();

            if (pullRequestNumber <= 0)
            {
                throw new ArgumentException("Invalid pull request number");
            }

            repository = ParseRepository(repository, _settings);

            var comments =
                await
                    GetData<Comment>(
                        _settings.BaseUrl.Replace("REPOSITORY", repository).Replace("OWNER", _settings.Owner) + "/pulls/" +
                        pullRequestNumber + "/comments", repository);
            
            return comments;
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

        private async Task<IEnumerable<T>> GetData<T>(string url, string repository) where T : class
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_settings.Username}:{_settings.Password}")));

                var items = new List<T>();
                var hasRecords = true;
                var pageNumber = 1;
                var pagedUrl = url + "?per_page=100&page=1";

                try
                {
                    while (hasRecords)
                    {
                        client.DefaultRequestHeaders.Add("User-Agent", repository);

                        var jsonData = await client.GetStringAsync(pagedUrl);

                        var newItems = JsonConvert.DeserializeObject<List<T>>(jsonData);

                        if (newItems.Any())
                        {
                            items.AddRange(newItems);

                            pageNumber++;
                            pagedUrl = url + "?per_page=100&page=" + pageNumber;
                        }
                        else
                        {
                            hasRecords = false;
                        }
                    }
                }
                catch (TaskCanceledException ex)
                {
                    throw new Exception("There was an error contacting GitHub", ex);
                }

                return items;
            }
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
    }
}