using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.Services;
using Microsoft.AspNetCore.Mvc;

namespace LCARS.Controllers
{
    public class DeploymentsController : Controller
    {
        private readonly IDeploymentsService _deploymentsService;

        public DeploymentsController(IDeploymentsService deploymentsService)
        {
            _deploymentsService = deploymentsService;
        }

        /// <remarks>Returns a the status of each configured deployment project and environment</remarks>
        /// <response code="200">Returns a the status of each configured deployment project and environment</response>
        /// <returns>Returns a the status of each configured deployment project and environment</returns>
        [ProducesResponseType(typeof(IEnumerable<ViewModels.Deployments.Deployment>), 200)]
        [HttpGet("/api/deployments")]
        public async Task<IActionResult> Get()
        {
            var vm = await _deploymentsService.Get();

            return Ok(vm);
        }
    }
}