using LCARS.Services;
using Microsoft.AspNetCore.Mvc;

namespace LCARS.Controllers
{
    public class EnvironmentsController : Controller
    {
        private readonly IEnvironmentsService _environmentsService;

        public EnvironmentsController(IEnvironmentsService environmentsService)
        {
            _environmentsService = environmentsService;
        }

        [HttpGet("/api/environments")]
        public IActionResult Get()
        {
            var vm = _environmentsService.Get();

            return Ok(vm);
        }
    }
}