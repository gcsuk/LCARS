using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.Services;
using Microsoft.AspNetCore.Mvc;

namespace LCARS.Controllers
{
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
        [ProducesResponseType(typeof(IEnumerable<ViewModels.GitHub.Branch>), 200)]
        [HttpGet("/api/github/branches/{repository}")]
        public async Task<IActionResult> GetBranches(string repository)
        {
            var branches = await _gitHubService.GetBranches(repository);

            return Ok(branches);
        }

        /// <remarks>Returns a list of all open pull requests for the configured repository</remarks>
        /// <response code="200">Returns a list of all open pull requests for the configured repository</response>
        /// <returns>A list of all open pull requests for the configured repository</returns>
        [ProducesResponseType(typeof(IEnumerable<ViewModels.GitHub.PullRequest>), 200)]
        [HttpGet("/api/github/pullrequests/{repository}")]
        public async Task<IActionResult> GetPullRequests(string repository)
        {
            var pullRequests = await _gitHubService.GetPullRequests(repository);

            return Ok(pullRequests);
        }

        /// <remarks>Returns a list of all PR comments for the configured repository</remarks>
        /// <response code="200">Returns a list of all PR comments for the configured repositor</response>
        /// <returns>A list of all PR comments for the configured repositor</returns>
        [ProducesResponseType(typeof(IEnumerable<ViewModels.GitHub.Comment>), 200)]
        [HttpGet("/api/github/comments/{repository}/{pullRequestNumber}")]
        public async Task<IActionResult> GetComments(string repository, int pullRequestNumber)
        {
            var comments = await _gitHubService.GetComments(repository, pullRequestNumber);

            return Ok(comments);
        }
    }
}