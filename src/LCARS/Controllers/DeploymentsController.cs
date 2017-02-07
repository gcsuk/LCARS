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

        [HttpGet("/api/deployments")]
        public IActionResult Get()
        {
            var vm = _deploymentsService.Get();

            return Ok(vm);
        }
    }
}