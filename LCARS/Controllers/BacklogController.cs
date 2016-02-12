using System;
using System.Linq;
using System.Web.Mvc;
using LCARS.Domain;
using LCARS.ViewModels.Issues;

namespace LCARS.Controllers
{
    public class BacklogController : Controller
    {
        private readonly IIssues _domain;

        public BacklogController(IIssues domain)
        {
            _domain = domain;
        }

        // GET: Backlog
        [Route("Backlog/{issueSet?}")]
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
                    .Single(i => i.Id == typeId);

            var vm = new Backlog
            {
                IssueSet = query.Name,
                BugList = _domain.Get(query.Jql),
                Deadline = query.Deadline
            };

            return View(vm);
        }

        [HttpGet]
        public JsonResult GetBacklog(int issueSet = 1)
        {
            var query =
                _domain.GetQueries(Server.MapPath(@"~/App_Data/Issues.json"))
                    .Single(i => i.Id == issueSet)
                    .Jql;

            return Json(_domain.Get(query), JsonRequestBehavior.AllowGet);
        }
    }
}