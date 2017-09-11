﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.Services;
using LCARS.ViewModels.Builds;
using Microsoft.AspNetCore.Mvc;

namespace LCARS.Controllers
{
    [Route("api/[controller]")]
    public class BuildsController : Controller
    {
        private readonly IBuildsService _buildsService;

        public BuildsController(IBuildsService buildsService)
        {
            _buildsService = buildsService;
        }

        /// <remarks>Returns all running builds</remarks>
        /// <response code="200">Returns running builds</response>
        /// <returns>A dictionary containing all running builds type ids and ids</returns>
        [ProducesResponseType(typeof(Dictionary<string, int>), 200)]
        [HttpGet("running")]
        public async Task<IActionResult> GetRunning()
        {
            var response = await _buildsService.GetBuildsRunning();

            return Ok(response);
        }

        /// <remarks>Returns the progress of a running build</remarks>
        /// <response code="200">Returns the progress of a running build</response>
        /// <returns>An object containing the current progress of the specified build</returns>
        [ProducesResponseType(typeof(BuildProgress), 200)]
        [HttpGet("progress/{buildId}")]
        public async Task<IActionResult> GetProgress(int buildId)
        {
            var response = await _buildsService.GetBuildProgress(buildId);

            var vm = new BuildProgress
            {
                Percentage = response.Percentage,
                Status = response.Status
            };

            return Ok(response);
        }

        /// <remarks>Returns the last build status of a specified type</remarks>
        /// <response code="200">Returns the last build status of a specified type</response>
        /// <returns>a key-value pair containing the percentage and the text status of the last build</returns>
        [ProducesResponseType(typeof(KeyValuePair<string, string>), 200)]
        [HttpGet("laststatus/{buildTypeId}")]
        public async Task<IActionResult> GetLastRunStatus(string buildTypeId)
        {
            var response = await _buildsService.GetLastBuildStatus(buildTypeId);

            return Ok(response);
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
                ServerPassword = settings.ServerPassword
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
                ServerPassword = settings.ServerPassword
            };

            await _buildsService.UpdateSettings(model);

            return NoContent();
        }
    }
}
