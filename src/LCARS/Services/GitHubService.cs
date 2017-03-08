using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LCARS.Repositories;
using LCARS.ViewModels.GitHub;
using Newtonsoft.Json;

namespace LCARS.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly DataContext _dbContext;
        private readonly Models.Settings _settings;

        public GitHubService(ISettingsService settingsService, DataContext dbContext)
        {
            _dbContext = dbContext;
            _settings = settingsService.GetSettings();
        }

        public async Task<IEnumerable<Branch>> GetBranches(string repository = null)
        {
            var settings = GetSettings();

            repository = ParseRepository(repository, settings);

            var branches = await GetData<Models.GitHub.Branch>(settings.BaseUrl.Replace("REPOSITORY", repository).Replace("OWNER", settings.Owner) + "/branches", repository);
            
            return branches.Select(b => new Branch
            {
                Name = b.Name
            });
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequests(string repository = null)
        {
            var settings = GetSettings();

            repository = ParseRepository(repository, settings);

            var pullRequests = await GetData<Models.GitHub.PullRequest>(settings.BaseUrl.Replace("REPOSITORY", repository).Replace("OWNER", settings.Owner) + "/pulls", repository);

            var result = pullRequests.Select(p => new PullRequest
            {
                Repository = repository,
                Number = p.Number,
                Title = p.Title,
                CreatedOn = p.CreatedOn,
                UpdatedOn = p.UpdatedOn,
                AuthorName = p.User.Name,
                AuthorAvatar = p.User.Avatar
            }).ToList();

            foreach (var pullRequest in result)
            {
                pullRequest.Comments = await GetComments(repository, pullRequest.Number);
            }

            return result;
        }

        public async Task<IEnumerable<Comment>> GetComments(string repository = null, int pullRequestNumber = 0)
        {
            if (pullRequestNumber <= 0)
            {
                throw new ArgumentException("Invalid pull request number");
            }

            var settings = GetSettings();

            repository = ParseRepository(repository, settings);

            var comments =
                await
                    GetData<Models.GitHub.Comment>(
                        settings.BaseUrl.Replace("REPOSITORY", repository).Replace("OWNER", settings.Owner) + "/pulls/" +
                        pullRequestNumber + "/comments", repository);
            
            return comments.Select(p => new Comment
            {
                DateCreated = p.DateCreated,
                User = new User
                {
                    Name = p.User.Name,
                    Avatar = p.User.Avatar
                },
                Body = p.Body
            });
        }

        public Settings GetSettings()
        {
            var settings = _dbContext.GitHubSettings.Select(t => new ViewModels.GitHub.Settings
            {
                BaseUrl = t.BaseUrl,
                Owner = t.Owner,
                Repositories = t.Repositories.ToList(),
                BranchThreshold = t.BranchThreshold,
                PullRequestThreshold = t.PullRequestThreshold
            }).SingleOrDefault();

            if (settings == null)
            {
                throw new InvalidOperationException("Invalid GitHub settings");
            }

            return settings;
        }

        private async Task<IEnumerable<T>> GetData<T>(string url, string repository) where T : class
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_settings.GitHubUsername}:{_settings.GitHubPassword}")));

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

        private static string ParseRepository(string repository, ViewModels.GitHub.Settings settings)
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