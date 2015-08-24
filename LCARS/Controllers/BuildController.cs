using System.Web.Mvc;
using LCARS.Models;
using LCARS.Services;
using LCARS.ViewModels;

namespace LCARS.Controllers
{
    public class BuildController : Controller
    {
        private readonly IDomain _domain;
        private readonly Board _thisBoard;

        public BuildController(IDomain domain)
        {
            _domain = domain;
            _thisBoard = Board.Build;
        }

        // GET: Build
        public ActionResult Index()
        {
            Board randomBoard = _domain.SelectBoard();

            if (_thisBoard != randomBoard)
            {
                return RedirectToAction("Index", randomBoard.GetDescription());
            }

            var vm = new BuildStatus
            {
                Builds = _domain.GetBuildStatus(Server.MapPath(@"~/App_Data/Builds.xml"))
            };

            return View(vm);
        }
    }
}