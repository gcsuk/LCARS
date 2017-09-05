using System;
using System.Collections.Generic;
using LCARS.Services;
using LCARS.ViewModels.Environments;
using Microsoft.AspNetCore.Mvc;
using Environment = LCARS.ViewModels.Environments.Environment;

namespace LCARS.Controllers
{
    [Route("api/[controller]")]
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
        [ProducesResponseType(typeof(IEnumerable<Environment>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            var vm = _environmentsService.GetSites();

            return Ok(vm);
        }

        /// <remarks>Adds a new site to the environments screen</remarks>
        /// <response code="200">New site added successfully</response>
        /// <returns>New site object, with ID populated</returns>
        [ProducesResponseType(typeof(Site), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Site site)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site), "site is null");

            var vm = _environmentsService.AddSite(site);

            return Ok(vm);
        }

        /// <remarks>Updates a site</remarks>
        /// <response code="200">Site updated successfully</response>
        [ProducesResponseType(204)]
        [HttpPut]
        public IActionResult Put([FromBody] Site site)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site), "site is null");

            _environmentsService.UpdateSite(site);

            return NoContent();
        }

        /// <remarks>Deletes a site</remarks>
        /// <response code="204">Site deleted successfully</response>
        [ProducesResponseType(204)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id), "ID must be non-negative");

            _environmentsService.DeleteSite(id);

            return NoContent();
        }
    }
}