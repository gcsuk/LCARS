using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LCARS.Services;
using LCARS.ViewModels.Builds;
using Microsoft.AspNetCore.Mvc;

namespace LCARS.Controllers
{

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
        [HttpGet("/api/builds/running")]
        public async Task<IActionResult> GetRunning()
        {
            var response = await _buildsService.GetBuildsRunning();

            return Ok(response);
        }

        /// <remarks>Returns the progress of a running build</remarks>
        /// <response code="200">Returns the progress of a running build</response>
        /// <returns>An object containing the current progress of the specified build</returns>
        [ProducesResponseType(typeof(BuildProgress), 200)]
        [HttpGet("/api/builds/progress/{buildId}")]
        public async Task<IActionResult> GetProgress(int buildId)
        {
            try
            {
                var response = await _buildsService.GetBuildProgress(buildId);

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(412, ex);
            }
        }

        /// <remarks>Returns the last build status of a specified type</remarks>
        /// <response code="200">Returns the last build status of a specified type</response>
        /// <returns>a key-value pair containing the percentage and the text status of the last build</returns>
        [ProducesResponseType(typeof(KeyValuePair<string, string>), 200)]
        [HttpGet("/api/builds/laststatus/{buildTypeId}")]
        public async Task<IActionResult> GetLastRunStatus(string buildTypeId)
        {
            try
            {
                var response = await _buildsService.GetLastBuildStatus(buildTypeId);

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(412, ex);
            }
        }
    }
}
