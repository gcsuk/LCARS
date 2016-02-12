using System;
using System.Linq;
using System.Web.Mvc;
using LCARS.Domain;

namespace LCARS.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssues _domain;

        public IssuesController(IIssues domain)
        {
            _domain = domain;
        }

        // GET: Issues
        [Route("Issues/{issueSet?}")]
        public ActionResult Index(int typeId = 0)
        {
            if (typeId == 0)
            {
                var issueSets = _domain.GetQueries(Server.MapPath(@"~/App_Data/Issues.json"))
                    .Select(q => q.Id)
                    .ToList();

                typeId = issueSets[new Random(Guid.NewGuid().GetHashCode()).Next(0, issueSets.Count)];
            }

            var query =
                _domain.GetQueries(Server.MapPath(@"~/App_Data/Issues.json"))
                    .Single(i => i.Id == typeId)
                    .Jql;

            var vm = new ViewModels.Issues.Issues
            {
                IssueList = _domain.Get(query)
            };

            return View(vm);
        }
    }
}