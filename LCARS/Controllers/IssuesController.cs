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

        public IssuesController(IIssues issuesDomain, IRedAlert commonDomain)
        {
            _issuesDomain = issuesDomain;
            _commonDomain = commonDomain;
        }

        // GET: Issues
        [Route("Issues/{issueSet?}")]
        public ActionResult Index(int typeId = 0)
        {
            if (typeId == 0)
            {
                var issueSets = _issuesDomain.GetQueries(Server.MapPath(@"~/App_Data/Issues.json"))
                    .Select(q => q.Id)
                    .ToList();

                typeId = issueSets[new Random(Guid.NewGuid().GetHashCode()).Next(0, issueSets.Count)];
            }

            var query =
                _issuesDomain.GetQueries(Server.MapPath(@"~/App_Data/Issues.json"))
                    .Single(i => i.Id == typeId)
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