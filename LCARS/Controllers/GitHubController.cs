using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCARS.Services;
using LCARS.ViewModels.GitHub;
using Microsoft.AspNetCore.Mvc;

namespace LCARS.Controllers
{
    [Route("api/[controller]")]
    public class GitHubController : Controller
    {
        private readonly IGitHubService _gitHubService;

        public GitHubController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        /// <remarks>Returns a list of all branches for the configured repository</remarks>
        /// <response code="200">Returns a list of all branches for the configured repository</response>
        /// <returns>A list of all branches for the configured repository</returns>
        [ProducesResponseType(typeof(IEnumerable<Branch>), 200)]
        [HttpGet("branches/{repository}")]
        public async Task<IActionResult> GetBranches(string repository)
        {
            var branches = await _gitHubService.GetBranches(repository);

            var vm = branches.Select(b => new Branch
            {
                Name = b.Name
            });

            return Ok(vm);
        }

        /// <remarks>Returns a list of all open pull requests for the configured repository</remarks>
        /// <response code="200">Returns a list of all open pull requests for the configured repository</response>
        /// <returns>A list of all open pull requests for the configured repository</returns>
        [ProducesResponseType(typeof(IEnumerable<PullRequest>), 200)]
        [HttpGet("pullrequests/{repository}")]
        public async Task<IActionResult> GetPullRequests(string repository)
        {
            var pullRequests = await _gitHubService.GetPullRequests(repository);

            var vm = pullRequests.Select(p => new PullRequest
            {
                Repository = repository,
                Number = p.Number,
                Title = p.Title,
                CreatedOn = p.CreatedOn,
                UpdatedOn = p.UpdatedOn,
                AuthorName = p.User.Name,
                AuthorAvatar = p.User.Avatar
            }).ToList();

            foreach (var pullRequest in vm)
            {
                var comments = await _gitHubService.GetComments(repository, pullRequest.Number);

                pullRequest.Comments = comments.Select(p => new Comment
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

            return Ok(vm);
        }

        /// <remarks>Returns a list of all PR comments for the configured repository</remarks>
        /// <response code="200">Returns a list of all PR comments for the configured repositor</response>
        /// <returns>A list of all PR comments for the configured repositor</returns>
        [ProducesResponseType(typeof(IEnumerable<Comment>), 200)]
        [HttpGet("comments/{repository}/{pullRequestNumber}")]
        public async Task<IActionResult> GetComments(string repository, int pullRequestNumber)
        {
            var comments = await _gitHubService.GetComments(repository, pullRequestNumber);

            var vm = comments.Select(p => new Comment
            {
                DateCreated = p.DateCreated,
                User = new User
                {
                    Name = p.User.Name,
                    Avatar = p.User.Avatar
                },
                Body = p.Body
            });

            return Ok(vm);
        }

        /// <remarks>Gets the configuration settings for GitHub</remarks>
        /// <response code="200">Settings returned successfully</response>
        [ProducesResponseType(200)]
        [HttpGet("settings")]
        public async Task<IActionResult> GetSettings()
        {
            var settings = await _gitHubService.GetSettings();

            var vm = new Settings
            {
                Id = settings.Id,
                Username = settings.Username,
                Password = settings.Password,
                BaseUrl = settings.BaseUrl,
                Owner = settings.Owner,
                Repositories = settings.Repositories.ToList(),
                BranchThreshold = settings.BranchThreshold,
                PullRequestThreshold = settings.PullRequestThreshold
            };

            return Ok(vm);
        }

        /// <remarks>Updates the configuration settings for GitHub</remarks>
        /// <response code="204">Settings successfully updated</response>
        [ProducesResponseType(204)]
        [HttpPut("settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] Settings settings)
        {
            var model = new Models.GitHub.Settings
            {
                Id = settings.Id,
                Username = settings.Username,
                Password = settings.Password,
                BaseUrl = settings.BaseUrl,
                BranchThreshold = settings.BranchThreshold,
                Owner = settings.Owner,
                PullRequestThreshold = settings.PullRequestThreshold,
                Repositories = settings.Repositories
            };

            await _gitHubService.UpdateSettings(model);

            return NoContent();
        }
    }
}