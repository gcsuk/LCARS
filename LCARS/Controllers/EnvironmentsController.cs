using System.Web.Mvc;
using LCARS.Domain;
using LCARS.ViewModels;

namespace LCARS.Controllers
{
	public class EnvironmentsController : Controller
	{
        private readonly ICommon _commonDomain;
		private readonly IEnvironments _environmentsDomain;
        private readonly Boards _thisBoard;

        public EnvironmentsController(ICommon commonDomain, IEnvironments environmentsDomain)
        {
            _commonDomain = commonDomain;
            _environmentsDomain = environmentsDomain;
            _thisBoard = Boards.Environment;
        }

		public ActionResult Index()
		{
            Boards randomBoard = _commonDomain.SelectBoard();

            if (_thisBoard != randomBoard)
            {
                return RedirectToAction("Index", randomBoard.GetDescription());
            }

            var vm = new ViewModels.Environments.Environments
            {
                Tenants = _environmentsDomain.Get(Server.MapPath(@"~/App_Data/Environments.xml")),
                IsRedAlertEnabled = _commonDomain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.xml")).IsEnabled
            };

            return View(vm);
        }

        [HttpPost]
        public void UpdateStatus(string tenant, string environment, string currentStatus)
        {
            _environmentsDomain.Update(Server.MapPath(@"~/App_Data/Environments.xml"), tenant, environment, currentStatus);
        }
	}
}