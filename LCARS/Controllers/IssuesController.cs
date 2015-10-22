using System.Web.Mvc;
using LCARS.Domain;
using LCARS.Models;

namespace LCARS.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssues _issuesDomain;
        private readonly ICommon _commonDomain;
        private readonly Boards _thisBoard;

        public IssuesController(IIssues issuesDomain, ICommon commonDomain)
        {
            _issuesDomain = issuesDomain;
            _commonDomain = commonDomain;
            _thisBoard = Boards.Issues;
        }

        // GET: Issues
        public ActionResult Index()
        {
            Boards randomBoard = _commonDomain.SelectBoard();

            if (_thisBoard != randomBoard)
            {
                return RedirectToAction("Index", randomBoard.GetDescription());
            }

            var vm = new ViewModels.Issues.Blockers
            {
                IssueList = _issuesDomain.Get(),
                IsRedAlertEnabled = _commonDomain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.xml")).IsEnabled
            };

            return View(vm);
        }
    }
}