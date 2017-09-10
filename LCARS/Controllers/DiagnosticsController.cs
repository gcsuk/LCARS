using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LCARS.Services;
using LCARS.ViewModels.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LCARS.Controllers
{
    /// <summary>
    /// Diagnostics for all services calling 3rd party endpoints
    /// </summary>
    public class DiagnosticsController : Controller
    {
        private readonly IBuildsService _buildsService;
        private readonly IDeploymentsService _deploymentsService;
        private readonly IGitHubService _gitHubService;
        private readonly IIssuesService _issuesService;

        public DiagnosticsController(IBuildsService buildsService, IDeploymentsService deploymentsService,
            IGitHubService gitHubService, IIssuesService issuesService)
        {
            _buildsService = buildsService;
            _deploymentsService = deploymentsService;
            _gitHubService = gitHubService;
            _issuesService = issuesService;
        }

        /// <remarks>Returns all running builds</remarks>
        /// <response code="200">Returns a test response from all external APIs</response>
        /// <returns>A list of all APIs and their current status</returns>
        [ProducesResponseType(typeof(Dictionary<string, int>), 200)]
        [HttpGet("/api/diagnostics")]
        public async Task<IActionResult> GetCurrentStatus()
        {
            var status = new DiagnosticsResult();

            var taskList = new Dictionary<string, Task>();

            try
            {
                taskList.Add(nameof(status.BuildsRunning), _buildsService.GetBuildsRunning());
                taskList.Add(nameof(status.BuildProgress), _buildsService.GetBuildsRunning().ContinueWith(prevTask =>
                {
                    prevTask.Wait();
                    _buildsService.GetBuildProgress(prevTask.Result.First().Value);
                }));
                taskList.Add(nameof(status.LastBuildStatus), _buildsService.GetBuildsRunning().ContinueWith(prevTask =>
                {
                    prevTask.Wait();
                    _buildsService.GetLastBuildStatus(prevTask.Result.First().Key);
                }));

                taskList.Add(nameof(status.Deployments), _deploymentsService.Get());

                taskList.Add(nameof(status.GitHubPullRequests), _gitHubService.GetPullRequests());
                taskList.Add(nameof(status.GitHubBranches), _gitHubService.GetBranches());

                var queries = await _issuesService.GetQueries();

                taskList.Add(nameof(status.Issues), _issuesService.GetIssues(queries.First().Jql));

                await Task.WhenAll(taskList.Values.ToArray());
            }
            catch
            {
                // Do nothing, everything is handled below
            }

            // Reflection!!
            foreach (var propertyInfo in typeof(DiagnosticsResult).GetProperties())
            {
                if (!taskList.Keys.Contains(propertyInfo.Name))
                    continue;

                var exception = taskList[propertyInfo.Name].Exception;

                typeof(DiagnosticsResult).GetProperty(propertyInfo.Name)
                    .SetValue(status, new DiagnosticsResult.DiagnosticsItem
                    {
                        Status = exception == null ? DiagnosticsStatus.OK : DiagnosticsStatus.Error,
                        ErrorMessage =
                            exception == null
                                ? ""
                                : exception.InnerExceptions != null && exception.InnerExceptions.Any()
                                    ? exception.InnerExceptions.First().Message
                                    : exception.Message
                    }, null);
            }

            return Ok(status);
        }
    }
}

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LCARS.Services;
using LCARS.ViewModels.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LCARS.Controllers
{
    /// <summary>
    /// Diagnostics for all services calling 3rd party endpoints
    /// </summary>
    public class DiagnosticsController : Controller
    {
        private readonly IBuildsService _buildsService;
        private readonly IDeploymentsService _deploymentsService;
        private readonly IGitHubService _gitHubService;
        private readonly IIssuesService _issuesService;

        public DiagnosticsController(IBuildsService buildsService, IDeploymentsService deploymentsService,
            IGitHubService gitHubService, IIssuesService issuesService)
        {
            _buildsService = buildsService;
            _deploymentsService = deploymentsService;
            _gitHubService = gitHubService;
            _issuesService = issuesService;
        }

        /// <remarks>Returns all running builds</remarks>
        /// <response code="200">Returns a test response from all external APIs</response>
        /// <returns>A list of all APIs and their current status</returns>
        [ProducesResponseType(typeof(Dictionary<string, int>), 200)]
        [HttpGet("/api/diagnostics")]
        public async Task<IActionResult> GetCurrentStatus()
        {
            var status = new DiagnosticsResult();

            var taskList = new Dictionary<string, Task>();

            Dictionary<string, int> buildsRunning = null;

            try
            {
                buildsRunning = await _buildsService.GetBuildsRunning();
            }
            catch (Exception ex)
            {
                status.BuildsRunning = new DiagnosticsResult.DiagnosticsItem
                {
                    Status = DiagnosticsStatus.Error,
                    ErrorMessage = ex.Message
                };
            }

            try
            {
                await _buildsService.GetBuildProgress(buildsRunning.First().Value);
            }
            catch (Exception ex)
            {
                status.BuildProgress = new DiagnosticsResult.DiagnosticsItem
                {
                    Status = DiagnosticsStatus.Error,
                    ErrorMessage = ex.Message
                };
            }

            try
            {
                await _buildsService.GetLastBuildStatus(buildsRunning.First().Key);
            }
            catch (Exception ex)
            {
                status.LastBuildStatus = new DiagnosticsResult.DiagnosticsItem
                {
                    Status = DiagnosticsStatus.Error,
                    ErrorMessage = ex.Message
                };
            }

            try
            {
                await _deploymentsService.Get();
            }
            catch (Exception ex)
            {
                status.Deployments = new DiagnosticsResult.DiagnosticsItem
                {
                    Status = DiagnosticsStatus.Error,
                    ErrorMessage = ex.Message
                };
            }

            try
            {
                await _gitHubService.GetPullRequests();
            }
            catch (Exception ex)
            {
                status.GitHubPullRequests = new DiagnosticsResult.DiagnosticsItem
                {
                    Status = DiagnosticsStatus.Error,
                    ErrorMessage = ex.Message
                };
            }

            try
            {
                await _gitHubService.GetBranches();
            }
            catch (Exception ex)
            {
                status.GitHubBranches = new DiagnosticsResult.DiagnosticsItem
                {
                    Status = DiagnosticsStatus.Error,
                    ErrorMessage = ex.Message
                };
            }

            try
            {
                var queries = _issuesService.GetQueries();

                await _issuesService.GetIssues(queries.First().Jql);
            }
            catch (Exception ex)
            {
                status.Issues = new DiagnosticsResult.DiagnosticsItem
                {
                    Status = DiagnosticsStatus.Error,
                    ErrorMessage = ex.Message
                };
            }

            return Ok(status);
        }
    }
} */