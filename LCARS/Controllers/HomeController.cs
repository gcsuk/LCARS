using System.Web.Mvc;
using LCARS.Domain;

namespace LCARS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var randomBoard = Settings.SelectBoard();

            return RedirectToAction("Index", randomBoard.GetDescription());
        }
    }
}