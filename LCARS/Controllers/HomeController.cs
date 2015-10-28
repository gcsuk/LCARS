using System.Web.Mvc;
using LCARS.Domain;
using LCARS.Models;

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
            Boards randomBoard = _commonDomain.SelectBoard();

            return RedirectToAction("Index", randomBoard.GetDescription());
        }
    }
}