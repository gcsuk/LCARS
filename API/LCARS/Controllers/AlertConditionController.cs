using System.Threading.Tasks;
using LCARS.Services;
using LCARS.ViewModels.AlertCondition;
using Microsoft.AspNetCore.Mvc;

namespace LCARS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertConditionController : ControllerBase
    {
        private readonly IAlertConditionService _alertConditionService;

        public AlertConditionController(IAlertConditionService alertConditionService)
        {
            _alertConditionService = alertConditionService;
        }

        /// <remarks>Returns the current alert condition</remarks>
        /// <response code="200">Returns the status</response>
        /// <returns>An object containing all details about the alert condition</returns>
        [ProducesResponseType(typeof(AlertCondition), 200)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _alertConditionService.GetAlertCondition();

            var vm = new AlertCondition
            {
                Condition = response.Condition,
                AlertType = response.AlertType,
                EndDate = response.EndDate
            };

            return Ok(vm);
        }
    }
}