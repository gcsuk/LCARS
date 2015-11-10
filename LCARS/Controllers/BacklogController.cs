using System;
using System.Linq;
using System.Web.Mvc;
using LCARS.Domain;
using LCARS.ViewModels.Issues;

namespace LCARS.Controllers
{
    public class BacklogController : Controller
    {
        private readonly IIssues _issuesDomain;
        private readonly IRedAlert _commonDomain;
        private readonly ViewModels.Boards _thisBoard;

        public BacklogController(IIssues issuesDomain, IRedAlert commonDomain)
        {
            _issuesDomain = issuesDomain;
            _commonDomain = commonDomain;
            _thisBoard = ViewModels.Boards.Issues;
        }

        // GET: Backlog
        [Route("Backlog/{issueSet?}")]
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
                    .Single(i => i.Id == issueSet);

            var vm = new Backlog
            {
                IssueSet = query.Name,
                BugList = _issuesDomain.Get(query.Jql),
                Deadline = query.Deadline,
                IsRedAlertEnabled = _commonDomain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json")).IsEnabled
            };

            return View(vm);
        }

        [HttpGet]
        public JsonResult GetBacklog(int issueSet = 1)
        {
            var query =
                _issuesDomain.GetQueries(Server.MapPath(@"~/App_Data/Issues.json"))
                    .Single(i => i.Id == issueSet)
                    .Jql;

            return Json(_issuesDomain.Get(query), JsonRequestBehavior.AllowGet);
        }
    }
}