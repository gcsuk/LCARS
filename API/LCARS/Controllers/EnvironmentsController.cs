using System;
using System.Collections.Generic;
using System.Linq;
using LCARS.Services;
using LCARS.ViewModels.Environments;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LCARS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnvironmentsController : ControllerBase
    {
        private readonly IEnvironmentsService _environmentsService;

        public EnvironmentsController(IEnvironmentsService environmentsService)
        {
            _environmentsService = environmentsService;
        }

        /// <remarks>Returns a the status of each configured environment</remarks>
        /// <response code="200">Returns a the status of each configured environment</response>
        /// <returns>Returns a the status of each configured environment</returns>
        [ProducesResponseType(typeof(IEnumerable<SiteEnvironment>), 200)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sites = await _environmentsService.GetSites();

            var vm = sites.Select(s => new Site
            {
                Id = s.Id,
                Name = s.Name,
                Environments = s.Environments.Select(e => new SiteEnvironment
                {
                    Id = e.Id,
                    SiteId = s.Id,
                    Name = e.Name,
                    SiteUrl = e.SiteUrl,
                    PingUrl = e.PingUrl,
                    Status = e.Status
                })
            });

            return Ok(vm);
        }

        /// <remarks>Adds a new site to the environments screen</remarks>
        /// <response code="200">New site added successfully</response>
        /// <returns>New site object, with ID populated</returns>
        [ProducesResponseType(typeof(Site), 200)]
        [HttpPost("site")]
        public async Task<IActionResult> PostSite([FromBody] Site vm)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(vm), "vm is null");

            var site = new Models.Environments.Site
            {
                Name = vm.Name,
                Environments = vm.Environments.Select(e => new Models.Environments.SiteEnvironment
                {
                    SiteId = vm.Id,
                    Name = e.Name,
                    SiteUrl = e.SiteUrl,
                    PingUrl = e.PingUrl
                }).ToList()
            };

            var newSite = await _environmentsService.AddSite(site);

            vm.Id = newSite.Id;

            foreach (var env in newSite.Environments)
            {
                vm.Environments.Single(e => e.Name == env.Name).Id = env.Id;
            }

            return Ok(vm);
        }

        /// <remarks>Adds a new environment to a site</remarks>
        /// <response code="200">New environment added successfully</response>
        /// <returns>New environment object, with ID populated</returns>
        [ProducesResponseType(typeof(Site), 200)]
        [HttpPost("environment")]
        public async Task<IActionResult> PostEnvironment([FromBody] SiteEnvironment vm)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(vm), "vm is null");

            var environment = new Models.Environments.SiteEnvironment
            {
                SiteId = vm.SiteId,
                Name = vm.Name,
                SiteUrl = vm.SiteUrl,
                PingUrl = vm.PingUrl
            };

            var newEnvironment = await _environmentsService.AddEnvironment(environment);

            vm.Id = newEnvironment.Id;

            return Ok(vm);
        }

        /// <remarks>Updates a site</remarks>
        /// <response code="200">Site updated successfully</response>
        [ProducesResponseType(204)]
        [HttpPut("site")]
        public async Task<IActionResult> PutSite([FromBody] Site vm)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(vm), "vm is null");

            var site = new Models.Environments.Site
            {
                Id = vm.Id,
                Name = vm.Name,
                Environments = vm.Environments.Select(e => new Models.Environments.SiteEnvironment
                {
                    Id = e.Id,
                    SiteId = vm.Id,
                    Name = e.Name,
                    SiteUrl = e.SiteUrl,
                    PingUrl = e.PingUrl
                }).ToList()
            };

            await _environmentsService.UpdateSite(site);

            return NoContent();
        }

        /// <remarks>Updates an environment</remarks>
        /// <response code="200">Environment updated successfully</response>
        [ProducesResponseType(204)]
        [HttpPut("environment")]
        public async Task<IActionResult> PutEnvironment([FromBody] SiteEnvironment vm)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(vm), "vm is null");

            var environment = new Models.Environments.SiteEnvironment
            {
                Id = vm.Id,
                Name = vm.Name,
                SiteUrl = vm.SiteUrl,
                PingUrl = vm.PingUrl
            };

            await _environmentsService.UpdateEnvironment(environment);

            return NoContent();
        }

        /// <remarks>Deletes a site</remarks>
        /// <response code="204">Site deleted successfully</response>
        [ProducesResponseType(204)]
        [HttpDelete("site/{id}")]
        public async Task<IActionResult> DeleteSite(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be non-negative", nameof(id));

            await _environmentsService.DeleteSite(id);

            return NoContent();
        }

        /// <remarks>Deletes a site</remarks>
        /// <response code="204">Site deleted successfully</response>
        [ProducesResponseType(204)]
        [HttpDelete("environment/{id}")]
        public async Task<IActionResult> DeleteEnvironment(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be non-negative", nameof(id));

            await _environmentsService.DeleteEnvironment(id);

            return NoContent();
        }
    }
}