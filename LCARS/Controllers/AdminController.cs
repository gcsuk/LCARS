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

		public ActionResult UpdateRedAlert()
		{
			RedAlert vm = _domain.GetRedAlertSettings(Server.MapPath(@"~/App_Data/RedAlert.xml"));

			return View(vm);
		}

		[HttpPost]
		public ActionResult UpdateRedAlert(bool isEnabled, string targetDate)
		{
			_domain.UpdateRedAlertSettings(Server.MapPath(@"~/App_Data/RedAlert.xml"), isEnabled, targetDate);

			return RedirectToAction(isEnabled ? "RedAlert" : "Index", "Home");
		}
    }
}