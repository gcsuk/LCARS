using System;
using System.Collections.Generic;
using LCARS.Services;
using LCARS.ViewModels.Environments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;

namespace LCARS.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
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
        [ProducesResponseType(typeof(IEnumerable<Models.Environments.Environment>), 200)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vm = await _environmentsService.GetSites();

            return Ok(vm);
        }

        /// <remarks>Adds a new site to the environments screen</remarks>
        /// <response code="200">New site added successfully</response>
        /// <returns>New site object, with ID populated</returns>
        [ProducesResponseType(typeof(Site), 200)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Site site)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site), "site is null");

            var vm = await _environmentsService.AddSite(site);

            return Ok(vm);
        }

        /// <remarks>Updates a site</remarks>
        /// <response code="200">Site updated successfully</response>
        [ProducesResponseType(204)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Site site)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site), "site is null");

            await _environmentsService.UpdateSite(site);

            return NoContent();
        }

        /// <remarks>Deletes a site</remarks>
        /// <response code="204">Site deleted successfully</response>
        [ProducesResponseType(204)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id), "ID must be non-negative");

            await _environmentsService.DeleteSite(id);

            return NoContent();
        }
    }
}