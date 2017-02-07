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

        [HttpGet("/api/github/branches/{repository}")]
        public IActionResult GetBranches(string repository)
        {
            var branches = _gitHubService.GetBranches(repository);

            return Ok(branches);
        }

        [HttpGet("/api/github/pullrequests/{repository}")]
        public IActionResult GetPullRequests(string repository)
        {
            var pullRequests = _gitHubService.GetPullRequests(repository);

            return Ok(pullRequests);
        }

        [HttpGet("/api/github/comments/{repository}/{pullRequestNumber}")]
        public IActionResult GetComments(string repository, int pullRequestNumber)
        {
            var comments = _gitHubService.GetComments(repository, pullRequestNumber);

            return Ok(comments);
        }
    }
}