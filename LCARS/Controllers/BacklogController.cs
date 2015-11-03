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
        public ActionResult Index(IssueSet issueSet = IssueSet.Random)
        {
            if (issueSet == IssueSet.Random)
            {
                var randomBoard = Settings.SelectBoard();

                if (_thisBoard != randomBoard)
                {
                    return RedirectToAction("Index", randomBoard.GetDescription());
                }

                issueSet = (IssueSet)new Random(Guid.NewGuid().GetHashCode()).Next(2, Enum.GetNames(typeof(IssueSet)).Length);
            }

            var query =
                _issuesDomain.GetQueries(Server.MapPath(@"~/App_Data/IssueQueries.json"))
                    .Single(i => i.Id == ((int)issueSet))
                    .Jql;

            var vm = new Bugs
            {
                IssueSet = issueSet.GetDescription(),
                BugList = _issuesDomain.Get(query),
                Deadline = new DateTime(2015, 11, 17, 17, 30, 0), // TODO: Sort this temporary hack
                IsRedAlertEnabled = _commonDomain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json")).IsEnabled
            };

            return View(vm);
        }

        [HttpGet]
        public JsonResult GetBacklog()
        {
            var query =
                _issuesDomain.GetQueries(Server.MapPath(@"~/App_Data/IssueQueries.json"))
                    .Single(i => i.Id == ((int)IssueSet.CmsBugs))
                    .Jql;

            return Json(_issuesDomain.Get(query), JsonRequestBehavior.AllowGet);
        }
    }
}