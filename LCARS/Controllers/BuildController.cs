using System.Web.Mvc;
using LCARS.Services;
using LCARS.ViewModels;

namespace LCARS.Controllers
{
    public class BuildController : Controller
    {
        private readonly IDomain _domain;

        public BuildController(IDomain domain)
        {
            _domain = domain;
        }

        // GET: Build
        public ActionResult Index()
        {
            var vm = new BuildStatus
            {
                Builds = _domain.GetBuildStatus(Server.MapPath(@"~/App_Data/Builds.xml"))
            };

            return View(vm);
        }
    }
}