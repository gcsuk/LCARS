using System;
using System.Linq;
using System.Web.Mvc;
using LCARS.Domain;

namespace LCARS.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssues _issuesDomain;
        private readonly IRedAlert _commonDomain;
        private readonly ViewModels.Boards _thisBoard;

        public IssuesController(IIssues issuesDomain, IRedAlert commonDomain)
        {
            _issuesDomain = issuesDomain;
            _commonDomain = commonDomain;
            _thisBoard = ViewModels.Boards.Issues;
        }

        // GET: Issues
        [Route("Issues/{issueSet?}")]
        public ActionResult Index(int issueSet = 0)
        {
            if (issueSet == 0)
            {
                var randomBoard = Settings.SelectBoard();

                if (_thisBoard != randomBoard)
                {
                    return RedirectToAction("Index", randomBoard.GetDescription());
                }

                var issueSets = _issuesDomain.GetQueries(Server.MapPath(@"~/App_Data/Issues.json"))
                    .Select(q => q.Id)
                    .ToList();

                issueSet = issueSets[new Random(Guid.NewGuid().GetHashCode()).Next(0, issueSets.Count)];
            }

            var query =
                _issuesDomain.GetQueries(Server.MapPath(@"~/App_Data/Issues.json"))
                    .Single(i => i.Id == issueSet)
                    .Jql;

            var vm = new ViewModels.Issues.Issues
            {
                IssueList = _issuesDomain.Get(query),
                IsRedAlertEnabled = _commonDomain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json")).IsEnabled
            };

            return View(vm);
        }
    }
}