using System.Web.Mvc;
using LCARS.Domain;

namespace LCARS.Controllers
{
    public class RedAlertController : Controller
    {
        private readonly ICommon _domain;

        public RedAlertController(ICommon domain)
        {
            _domain = domain;
        }

        // GET: RedAlert
        public ActionResult Index()
        {
            return View(_domain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.xml")));
        }

        public ActionResult Update()
        {
            var vm = _domain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.xml"));

            return View(vm);
        }

        [HttpPost]
        public ActionResult Update(bool isEnabled, string targetDate, string alertType)
        {
            _domain.UpdateRedAlert(Server.MapPath(@"~/App_Data/RedAlert.xml"), isEnabled, targetDate, alertType);

            return RedirectToAction("Index", isEnabled ? "RedAlert" : "Home");
        }
    }
}