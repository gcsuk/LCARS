using System.Web.Mvc;
using LCARS.Models;
using LCARS.Services;
using LCARS.ViewModels;

namespace LCARS.Controllers
{
    public class BuildController : Controller
    {
        private readonly IDomain _domain;
        private readonly Boards _thisBoard;

        public BuildController(IDomain domain)
        {
            _domain = domain;
            _thisBoard = Boards.Build;
        }

        // GET: Build
        public ActionResult Index()
        {
            Boards randomBoard = _domain.SelectBoard();

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