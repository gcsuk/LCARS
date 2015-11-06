using System.Web.Mvc;
using LCARS.Domain;

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
    }
}