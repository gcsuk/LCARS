using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.Services;
using Microsoft.AspNetCore.Mvc;
using LCARS.ViewModels.Deployments;
using System.Linq;

namespace LCARS.Controllers
{
    [Route("api/[controller]")]
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
        [ProducesResponseType(typeof(IEnumerable<Deployment>), 200)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var deployments = await _deploymentsService.Get();

            var vm = deployments.Deploys.Select(deployment => new Deployment
            {
                ProjectGroupId = deployments.Projects.Single(g => g.Id == deployment.ProjectId).ProjectGroupId,
                ProjectGroup = deployments.ProjectGroups.Single(pg => pg.Id == deployments.Projects.Single(g => g.Id == deployment.ProjectId).ProjectGroupId).Name,
                ProjectId = deployment.ProjectId,
                Project = deployment.Project,
                EnvironmentId = deployment.EnvironmentId,
                Environment = deployment.Environment,
                CompletedTime = deployment.CompletedTime,
                Duration = deployment.Duration,
                State = deployment.State,
                HasWarningsOrErrors = deployment.HasWarningsOrErrors,
                ReleaseVersion = deployment.ReleaseVersion
            }).ToList();

            return Ok(vm);
        }

        /// <remarks>Gets the configuration settings for deployments</remarks>
        /// <response code="200">Settings returned successfully</response>
        [ProducesResponseType(200)]
        [HttpGet("settings")]
        public async Task<IActionResult> GetSettings()
        {
            var settings = await _deploymentsService.GetSettings();

            var vm = new Settings
            {
                Id = settings.Id,
                ServerUrl = settings.ServerUrl,
                ServerKey = settings.ServerKey
            };

            return Ok(vm);
        }

        /// <remarks>Updates the configuration settings for deployments</remarks>
        /// <response code="204">Settings successfully updated</response>
        [ProducesResponseType(204)]
        [HttpPut("settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] Settings settings)
        {
            var model = new Models.Deployments.Settings
            {
                Id = settings.Id,
                ServerUrl = settings.ServerUrl,
                ServerKey = settings.ServerKey
            };

            await _deploymentsService.UpdateSettings(model);

            return NoContent();
        }
    }
}