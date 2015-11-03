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
        public ActionResult Index()
        {
            var randomBoard = Settings.SelectBoard();

            if (_thisBoard != randomBoard)
            {
                return RedirectToAction("Index", randomBoard.GetDescription());
            }

            var vm = new ViewModels.Issues.Blockers
            {
                IssueList = _issuesDomain.Get(),
                IsRedAlertEnabled = _commonDomain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json")).IsEnabled
            };

            return View(vm);
        }
    }
}