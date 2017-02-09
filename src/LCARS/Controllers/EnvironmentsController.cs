using System.Collections.Generic;
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

        /// <remarks>Returns a the status of each configured environment</remarks>
        /// <response code="200">Returns a the status of each configured environment</response>
        /// <returns>Returns a the status of each configured environment</returns>
        [ProducesResponseType(typeof(IEnumerable<ViewModels.Environments.Environment>), 200)]
        [HttpGet("/api/environments")]
        public IActionResult Get()
        {
            var vm = _environmentsService.Get();

            return Ok(vm);
        }
    }
}