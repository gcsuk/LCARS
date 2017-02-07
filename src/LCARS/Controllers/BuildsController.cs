using System;
using System.Threading.Tasks;
using LCARS.Services;
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

        [HttpGet("/api/builds/running")]
        public async Task<IActionResult> GetRunning()
        {
            try
            {
                var response = await _buildsService.GetBuildsRunning();

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(412, ex);
            }
        }

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

        [HttpGet("/api/builds/laststatus/{buildTypeId}")]
        public async Task<IActionResult> GetRunning(string buildTypeId)
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
