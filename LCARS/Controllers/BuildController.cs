using System.Web.Mvc;
using LCARS.Models;
using LCARS.Services;

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
        [Route("Builds/{buildSet?}")]
        public ActionResult Index(BuildSet buildSet = BuildSet.Cms)
        {
            var buildStatus =
                _domain.GetBuildStatus(Server.MapPath($@"~/App_Data/BuildSets/{buildSet.GetDescription()}.xml"));

            if (!buildStatus.IsStatic)
            {
                Boards randomBoard = _domain.SelectBoard();

                if (_thisBoard != randomBoard)
                {
                    return RedirectToAction("Index", randomBoard.GetDescription());
                }
            }

            var vm = new ViewModels.BuildStatus
            {
                Builds = buildStatus.Builds,
                IsRedAlertEnabled = _domain.GetRedAlertSettings(Server.MapPath(@"~/App_Data/RedAlert.xml")).IsEnabled
            };

            return View(buildSet.GetDescription(), vm);
        }
    }
}