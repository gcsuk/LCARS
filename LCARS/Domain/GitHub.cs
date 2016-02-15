using System.Collections.Generic;
using System.Linq;
using LCARS.ViewModels.GitHub;
using LCARS.Services;

namespace LCARS.Domain
{
    public class GitHub : IGitHub
    {
        private readonly Repository.IRepository<Models.GitHub.Settings> _settingsRepository;
        private readonly IGitHub<Models.GitHub.Branch> _branchService;
        private readonly IGitHub<Models.GitHub.PullRequest> _pullRequestService;
        private readonly IGitHub<Models.GitHub.Comment> _commentService;

        public GitHub(Repository.IRepository<Models.GitHub.Settings> settingsRepository,
            IGitHub<Models.GitHub.Branch> branchService, IGitHub<Models.GitHub.PullRequest> pullRequestService, IGitHub<Models.GitHub.Comment> commentService)
        {
            _settingsRepository = settingsRepository;
            _branchService = branchService;
            _pullRequestService = pullRequestService;
            _commentService = commentService;
        }

        public ViewModels.GitHub.Settings GetSettings(string filePath)
        {
            var settings = _settingsRepository.Get(filePath);

            return new ViewModels.GitHub.Settings
            {
                Owner = settings.Owner,
                BaseUrl = settings.BaseUrl,
                Repositories = settings.Repositories
            };
        }

        public IEnumerable<Branch> GetBranches(string url, string repository)
        {
            return _branchService.Get(url, repository).Select(b => new Branch
            {
                Name = b.Name
            });
        }

        public IEnumerable<PullRequest> GetPullRequests(string url, string repository)
        {
            return
                _pullRequestService.Get(url.Replace("REPOSITORY", repository), repository).Select(p => new PullRequest
                {
                    Repository = repository,
                    Number = p.Number,
                    Title = p.Title,
                    CreatedOn = p.CreatedOn,
                    UpdatedOn = p.UpdatedOn,
                    AuthorName = p.User.Name,
                    AuthorAvatar = p.User.Avatar /*,
                    CommentCount = _pullRequestService.GetCount(url.Replace("REPOSITORY", repository) + "/" + p.Number + "/comments", repository)*/
                });
        }

        public IEnumerable<string> GetComments(string url, string repository)
        {
            return _commentService.Get(url.Replace("REPOSITORY", repository), repository).Select(p => p.Body);
        }
    }
}