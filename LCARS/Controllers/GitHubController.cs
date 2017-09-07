using System.Collections.Generic;
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

            return Ok(branches);
        }

        /// <remarks>Returns a list of all open pull requests for the configured repository</remarks>
        /// <response code="200">Returns a list of all open pull requests for the configured repository</response>
        /// <returns>A list of all open pull requests for the configured repository</returns>
        [ProducesResponseType(typeof(IEnumerable<PullRequest>), 200)]
        [HttpGet("pullrequests/{repository}")]
        public async Task<IActionResult> GetPullRequests(string repository)
        {
            var pullRequests = await _gitHubService.GetPullRequests(repository);

            return Ok(pullRequests);
        }

        /// <remarks>Returns a list of all PR comments for the configured repository</remarks>
        /// <response code="200">Returns a list of all PR comments for the configured repositor</response>
        /// <returns>A list of all PR comments for the configured repositor</returns>
        [ProducesResponseType(typeof(IEnumerable<Comment>), 200)]
        [HttpGet("comments/{repository}/{pullRequestNumber}")]
        public async Task<IActionResult> GetComments(string repository, int pullRequestNumber)
        {
            var comments = await _gitHubService.GetComments(repository, pullRequestNumber);

            return Ok(comments);
        }

        /// <remarks>Gets the configuration settings for GitHub</remarks>
        /// <response code="200">Settings returned successfully</response>
        [ProducesResponseType(200)]
        [HttpGet("settings")]
        public async Task<IActionResult> GetSettings()
        {
            var vm = _gitHubService.GetSettings();

            return Ok(vm);
        }

        /// <remarks>Updates the configuration settings for GitHub</remarks>
        /// <response code="204">Settings successfully updated</response>
        [ProducesResponseType(204)]
        [HttpPut("settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] Settings settings)
        {
            _gitHubService.UpdateSettings(settings);

            return NoContent();
        }
    }
}