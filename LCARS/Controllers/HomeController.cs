using System.Web.Mvc;
using LCARS.Services;
using LCARS.ViewModels;

namespace LCARS.Controllers
{
	public class HomeController : Controller
	{
		private readonly IDomain _domain;

		public HomeController(IDomain domain)
		{
			_domain = domain;
		}

		public ActionResult Index()
		{
		    Status vm = new Status
		    {
		        Tenants = _domain.GetStatus(Server.MapPath(@"~/App_Data/Status.xml")),
		        IsAutoDeployEnabled = _domain.GetAutoDeploySettings(Server.MapPath(@"~/App_Data/AutoDeploy.xml")).IsEnabled
		    };

			return View(vm);
		}

		public ActionResult AutoDeploy()
		{
			return View(_domain.GetAutoDeploySettings(Server.MapPath(@"~/App_Data/AutoDeploy.xml")));
		}
	}
}