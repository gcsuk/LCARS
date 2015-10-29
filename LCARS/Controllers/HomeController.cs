using System.Web.Mvc;
using LCARS.Domain;

namespace LCARS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommon _commonDomain;

        public HomeController(ICommon commonDomain)
        {
            _commonDomain = commonDomain;
        }

        public ActionResult Index()
        {
            var randomBoard = _commonDomain.SelectBoard();

            return RedirectToAction("Index", randomBoard.GetDescription());
        }
    }
}