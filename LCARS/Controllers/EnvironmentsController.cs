using System.Web.Mvc;
using LCARS.Domain;

namespace LCARS.Controllers
{
	public class EnvironmentsController : Controller
	{
		private readonly IEnvironments _domain;

        public EnvironmentsController(IEnvironments domain)
        {
            _domain = domain;
        }

		public ActionResult Index()
		{
            var vm = new ViewModels.Environments.Environments
            {
                Tenants = _domain.Get(Server.MapPath(@"~/App_Data/Environments.json"))
            };

            return View(vm);
        }

        [HttpPost]
        public void UpdateStatus(string tenant, string environment, string currentStatus)
        {
            _domain.Update(Server.MapPath(@"~/App_Data/Environments.json"), tenant, environment, currentStatus);
        }
	}
}