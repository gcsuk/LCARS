using System.Web.Mvc;
using LCARS.Models;
using LCARS.Services;
using LCARS.ViewModels;

namespace LCARS.Controllers
{
	public class HomeController : Controller
	{
		private readonly IDomain _domain;
        private readonly Boards _thisBoard;

        public HomeController(IDomain domain)
		{
			_domain = domain;
            _thisBoard = Boards.Environment;
        }

		public ActionResult Index()
		{
            Boards randomBoard = _domain.SelectBoard();

            if (_thisBoard != randomBoard)
            {
                return RedirectToAction("Index", randomBoard.GetDescription());
            }

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