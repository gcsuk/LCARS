using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCARS.Services;
using LCARS.ViewModels.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LCARS.Controllers
{

    public class DiagnosticsController : Controller
    {
        private readonly IBuildsService _buildsService;
        private readonly IDeploymentsService _deploymentsService;
        private readonly IEnvironmentsService _environmentsService;
        private readonly IGitHubService _gitHubService;
        private readonly IIssuesService _issuesService;

        public DiagnosticsController(IBuildsService buildsService, IDeploymentsService deploymentsService,
            IEnvironmentsService environmentsService, IGitHubService gitHubService, IIssuesService issuesService)
        {
            _buildsService = buildsService;
            _deploymentsService = deploymentsService;
            _environmentsService = environmentsService;
            _gitHubService = gitHubService;
            _issuesService = issuesService;
        }

        /// <remarks>Returns all running builds</remarks>
        /// <response code="200">Returns a test response from all external APIs</response>
        /// <returns>A list of all APIs and their current status</returns>
        [ProducesResponseType(typeof(Dictionary<string, int>), 200)]
        [HttpGet("/api/diagnostics")]
        public IActionResult GetRunning()
        {
            var taskList = new List<Task>();

            var status = new DiagnosticsResult();

            try
            {
                var queries = _issuesService.GetQueries();
                taskList.Add(_issuesService.GetIssues(queries.First().Jql));

                taskList.Add(_buildsService.GetBuildsRunning());
                taskList.Add(_buildsService.GetBuildProgress(1234));
                taskList.Add(_buildsService.GetLastBuildStatus("bt121"));

                taskList.Add(_deploymentsService.Get());

                var gitHubSettings = _gitHubService.GetSettings();

                if (!gitHubSettings.Repositories.Any())
                {
                    status.GitHubPullRequests = new DiagnosticsResult.DiagnosticsItem
                    {
                        Status = DiagnosticsStatus.Warning,
                        ErrorMessage = "No repositories configured"
                    };
                }

                var repo = gitHubSettings.Repositories[0].Replace("\"", "");

                taskList.Add(_gitHubService.GetPullRequests(repo));
                taskList.Add(_gitHubService.GetBranches(repo));
                taskList.Add(_gitHubService.GetComments(repo, 1234));

                Task.WaitAll(taskList.ToArray());

                status.BuildsRunning = new DiagnosticsResult.DiagnosticsItem
                {
                    Status = DiagnosticsStatus.OK
                };
            }
            catch (Exception ex)
            {

                return Ok();
            }

            //try
            //{
            //    var buildProgress = _buildsService.GetBuildProgress(1234);

            //    taskList.Add(buildProgress);

            //    status.BuildProgress = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.OK
            //    };
            //}
            //catch (Exception ex)
            //{
            //    status.BuildProgress = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.Error,
            //        ErrorMessage = ex.Message
            //    };
            //}

            //try
            //{
            //    var lastBuildStatus = _buildsService.GetLastBuildStatus("bt121");

            //    taskList.Add(lastBuildStatus);

            //    status.LastBuildStatus = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.OK
            //    };
            //}
            //catch (Exception ex)
            //{
            //    status.LastBuildStatus = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.Error,
            //        ErrorMessage = ex.Message
            //    };
            //}

            //try
            //{
            //    var deployments = _deploymentsService.Get();

            //    taskList.Add(deployments);

            //    status.Deployments = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.OK
            //    };
            //}
            //catch (Exception ex)
            //{
            //    status.Deployments = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.Error,
            //        ErrorMessage = ex.Message
            //    };
            //}

            //try
            //{
            //    _environmentsService.Get();

            //    status.Environments = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.OK
            //    };
            //}
            //catch (Exception ex)
            //{
            //    status.Environments = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.Error,
            //        ErrorMessage = ex.Message
            //    };
            //}

            

            

            //try
            //{
            //    var pullRequests = _gitHubService.GetPullRequests(repo);

            //    taskList.Add(pullRequests);

            //    status.GitHubPullRequests = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.OK
            //    };
            //}
            //catch (Exception ex)
            //{
            //    status.GitHubPullRequests = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.Error,
            //        ErrorMessage = ex.Message
            //    };
            //}

            //try
            //{
            //    var branches = _gitHubService.GetBranches(repo);

            //    taskList.Add(branches);

            //    status.GitHubBranches = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.OK
            //    };
            //}
            //catch (Exception ex)
            //{
            //    status.GitHubBranches = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.Error,
            //        ErrorMessage = ex.Message
            //    };
            //}

            //try
            //{
            //    var comments = _gitHubService.GetComments(repo, 1234);

            //    taskList.Add(comments);

            //    status.GitHubComments = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.OK
            //    };
            //}
            //catch (Exception ex)
            //{
            //    status.GitHubComments = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.Error,
            //        ErrorMessage = ex.Message
            //    };
            //}

            //try
            //{
            //    var queries = _issuesService.GetQueries();

            //    var issues = _issuesService.GetIssues(queries.First().Jql);

            //    taskList.Add(issues);

            //    status.Deployments = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.OK
            //    };
            //}
            //catch (Exception ex)
            //{
            //    status.Deployments = new DiagnosticsResult.DiagnosticsItem
            //    {
            //        Status = DiagnosticsStatus.Error,
            //        ErrorMessage = ex.Message
            //    };
            //}


            return Ok(status);
        }
    }
}