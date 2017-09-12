﻿using System.Linq;
using System.Threading.Tasks;
using LCARS.Services;
using Microsoft.AspNetCore.Mvc;
using LCARS.ViewModels.Issues;
using System;

namespace LCARS.Controllers
{
    [Route("api/[controller]")]
    public class IssuesController : Controller
    {
        private readonly IIssuesService _issuesService;

        public IssuesController(IIssuesService issuesService)
        {
            _issuesService = issuesService;
        }

        /// <remarks>Returns a list of all issues for a stored query</remarks>
        /// <response code="200">Returns a list of issues resulting from the stored query</response>
        /// <returns>A list of issues resulting from the stored query</returns>
        [ProducesResponseType(typeof(Issues), 200)]
        [HttpGet("{queryId}")]
        public async Task<IActionResult> GetIssues(int queryId)
        {
            if (queryId <= 0)
                throw new ArgumentException("Invalid query ID", nameof(queryId));

            var query = (await _issuesService.GetQueries(queryId)).SingleOrDefault();

            if (query == null)
                return NotFound();

            var issues = await _issuesService.GetIssues(query.Jql);

            var vm = new Issues
            {
                IssueList = issues.OrderByDescending(i => i.Fields.Created).Select(i => new Issue
                {
                    Id = i.Key,
                    Summary = i.Fields.Summary,
                    Status = i.Fields.Status == null ? "N/A" : i.Fields.Status.Name,
                    Reporter = i.Fields.Reporter == null ? "N/A" : i.Fields.Reporter.DisplayName,
                    Priority = i.Fields.Priority == null ? "N/A" : i.Fields.Priority.Name,
                    PriorityIcon = i.Fields.Priority == null ? "N/At" : i.Fields.Priority.IconUrl,
                    Assignee = i.Fields.Assignee == null ? "N/A" : i.Fields.Assignee.DisplayName
                })
            };

            return Ok(vm);
        }

        /// <response code="200">Returns a summary of backlog for a specified query</response>
        [ProducesResponseType(typeof(Summary), 200)]
        [HttpGet("summary/{queryId}")]
        public async Task<IActionResult> GetSummary(int queryId)
        {
            if (queryId <= 0)
                throw new ArgumentException("Invalid query ID", nameof(queryId));

            var query = (await _issuesService.GetQueries(queryId)).SingleOrDefault();

            if (query == null)
                return NotFound();

            var issues = await _issuesService.GetIssues(query.Jql);

            var vm = new Summary
            {
                IssueSet = query.Name,
                Deadline = query.Deadline,
                IssueCount = issues.Count()
            };

            return Ok(vm);
        }

        /// <remarks>Returns a list of all issues for a stored query</remarks>
        /// <response code="200">Returns a list of issues resulting from the stored query</response>
        /// <returns>A list of issues resulting from the stored query</returns>
        [ProducesResponseType(typeof(Issues), 200)]
        [HttpGet("queries")]
        public async Task<IActionResult> GetQueries()
        {
            var queries = await _issuesService.GetQueries();

            if (queries == null || !queries.Any())
                return NotFound();

            var vm = queries.Select(q => new Query
            {
                Id = q.Id,
                Name = q.Name,
                Deadline = q.Deadline,
                Jql = q.Jql
            });

            return Ok(vm);
        }

        /// <remarks>Updates a query</remarks>
        /// <response code="204">Query updated successfully</response>
        [ProducesResponseType(204)]
        [HttpPut("queries")]
        public async Task<IActionResult> UpdateQuery([FromBody] Query query)
        {
            var model = new Models.Issues.Query
            {
                Id = query.Id,
                Name = query.Name,
                Deadline = query.Deadline,
                Jql = query.Jql
            };

            await _issuesService.UpdateQuery(model);

            return NoContent();
        }

        /// <remarks>Gets the configuration settings for issues</remarks>
        /// <response code="200">Settings returned successfully</response>
        [ProducesResponseType(200)]
        [HttpGet("settings")]
        public async Task<IActionResult> GetSettings()
        {
            var settings = await _issuesService.GetSettings();

            var vm = new Settings
            {
                Id = settings.Id,
                Url = settings.Url,
                Username = settings.Username,
                Password = settings.Password
            };

            return Ok(vm);
        }

        /// <remarks>Updates the configuration settings for issues</remarks>
        /// <response code="204">Settings successfully updated</response>
        [ProducesResponseType(204)]
        [HttpPut("settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] Settings settings)
        {
            var model = new Models.Issues.Settings
            {
                Id = settings.Id,
                Username = settings.Username,
                Password = settings.Password,
                Url = settings.Url
            };

            await _issuesService.UpdateSettings(model);

            return NoContent();
        }
    }
}