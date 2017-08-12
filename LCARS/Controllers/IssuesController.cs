using System.Linq;
using System.Threading.Tasks;
using LCARS.Services;
using Microsoft.AspNetCore.Mvc;

namespace LCARS.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssuesService _issuesService;

        public IssuesController(IIssuesService issuesService)
        {
            _issuesService = issuesService;
        }

        /// <remarks>Returns a list of all issues for a stored query</remarks>
        /// <response code="200">Returns a list of issues resulting from the stored query</response>
        /// <returns>A list of issues resulting from the stored query</returns>
        [ProducesResponseType(typeof(ViewModels.Issues.Issues), 200)]
        [HttpGet("/api/issues/{typeId?}")]
        public async Task<IActionResult> Get(int? typeId = null)
        {
            var query = _issuesService.GetQueries(typeId).FirstOrDefault();

            if (query == null)
            {
                return StatusCode(412, "No matching queries!");
            }

            var vm = new ViewModels.Issues.Issues
            {
                IssueList = await _issuesService.GetIssues(query.Jql)
            };

            return Ok(vm);
        }
    }
}