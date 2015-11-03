using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LCARS.Domain;

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
        public ActionResult Index()
        {
            var randomBoard = Settings.SelectBoard();

            if (_thisBoard != randomBoard)
            {
                return RedirectToAction("Index", randomBoard.GetDescription());
            }

            var query =
                _issuesDomain.GetQueries(Server.MapPath(@"~/App_Data/IssueQueries.json"))
                    .Single(i => i.Id == ((int)ViewModels.Issues.IssueSet.Bugs))
                    .Jql;

            var vm = new ViewModels.Issues.Bugs
            {
                BugList = _issuesDomain.Get(query),
                IsRedAlertEnabled = _commonDomain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json")).IsEnabled
            };

            return View(vm);
        }
    }
}