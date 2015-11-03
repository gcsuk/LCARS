using System.Web.Mvc;
using LCARS.Domain;
using LCARS.ViewModels;

namespace LCARS.Controllers
{
    public class RedAlertController : Controller
    {
        private readonly IRedAlert _domain;

        public RedAlertController(IRedAlert domain)
        {
            _domain = domain;
        }

        // GET: RedAlert
        public ActionResult Index()
        {
            return View(_domain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json")));
        }

        public ActionResult Update()
        {
            var vm = _domain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json"));

            return View(vm);
        }

        [HttpPost]
        public ActionResult Update(bool isEnabled, string targetDate, string alertType)
        {
            var settings = new ViewModels.RedAlert
            {
                IsEnabled = isEnabled,
                TargetDate = targetDate,
                AlertType = alertType
            };

            _domain.UpdateRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json"), settings);

            return RedirectToAction("Index", isEnabled ? "RedAlert" : "Home");
        }
    }
}