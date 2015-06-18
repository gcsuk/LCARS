using System.Web.Mvc;
using LCARS.Models;
using LCARS.Services;

namespace LCARS.Controllers
{
    public class AdminController : Controller
    {
		private readonly IDomain _domain;

		public AdminController(IDomain domain)
		{
			_domain = domain;
		}

		[HttpPost]
		public void UpdateStatus(string tenant, string dependency, string environment, string currentStatus)
		{
			_domain.UpdateStatus(Server.MapPath(@"~/App_Data/Status.xml"), tenant, dependency, environment, currentStatus);
		}

		public ActionResult UpdateAutoDeploy()
		{
			AutoDeploy vm = _domain.GetAutoDeploySettings(Server.MapPath(@"~/App_Data/AutoDeploy.xml"));

			return View(vm);
		}

		[HttpPost]
		public ActionResult UpdateAutoDeploy(bool isEnabled, string targetDate)
		{
			_domain.UpdateAutoDeploySettings(Server.MapPath(@"~/App_Data/AutoDeploy.xml"), isEnabled, targetDate);

			return RedirectToAction(isEnabled ? "AutoDeploy" : "Index", "Home");
		}
    }
}