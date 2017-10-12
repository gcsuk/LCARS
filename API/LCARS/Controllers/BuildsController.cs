using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCARS.Services;
using LCARS.ViewModels.Builds;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LCARS.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class BuildsController : Controller
    {
        private readonly IBuildsService _buildsService;

        public BuildsController(IBuildsService buildsService)
        {
            _buildsService = buildsService;
        }

        /// <remarks>Returns all builds</remarks>
        /// <response code="200">Returns all builds</response>
        /// <returns>A dictionary containing all builds</returns>
        [ProducesResponseType(typeof(IEnumerable<Build>), 200)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _buildsService.GetBuilds();

            var vm = new List<Build>();

            response.ToList().ForEach(x =>
            {
                vm.Add(new Build
                {
                    Id = x.Id,
                    Name = x.BuildTypeName,
                    Version = x.Number,
                    Status = x.Status,
                    State = x.State,
                    PercentageComplete = x.PercentageComplete
                });
            });

            return Ok(vm);
        }

        /// <remarks>Returns specified build details</remarks>
        /// <response code="200">Returns specified build</response>
        /// <returns>A dictionary containing specified build id</returns>
        [ProducesResponseType(typeof(Build), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _buildsService.GetBuild(id);

            var vm = new BuildProgress
            {
                Id = response.Id,
                Version = response.Number,
                Status = response.Status,
                State = response.State,
                PercentageComplete = response.PercentageComplete
            };

            return Ok(vm);
        }

        /// <remarks>Gets the configuration settings for builds</remarks>
        /// <response code="200">Settings returned successfully</response>
        [ProducesResponseType(200)]
        [HttpGet("settings")]
        public async Task<IActionResult> GetSettings()
        {
            var settings = await _buildsService.GetSettings();

            var vm = new Settings
            {
                Id = settings.Id,
                ServerUrl = settings.ServerUrl,
                ServerUsername = settings.ServerUsername,
                ServerPassword = settings.ServerPassword,
                ProjectIds = settings.ProjectIds.Split(",")
            };

            return Ok(vm);
        }

        /// <remarks>Updates the configuration settings for builds</remarks>
        /// <response code="204">Settings successfully updated</response>
        [ProducesResponseType(204)]
        [HttpPut("settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] Settings settings)
        {
            var model = new Models.Builds.Settings
            {
                Id = settings.Id,
                ServerUrl = settings.ServerUrl,
                ServerUsername = settings.ServerUsername,
                ServerPassword = settings.ServerPassword,
                ProjectIds = string.Join(",", settings.ProjectIds.ToArray())
            };

            await _buildsService.UpdateSettings(model);

            return NoContent();
        }
    }
}