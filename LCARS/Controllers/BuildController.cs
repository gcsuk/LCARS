using System;
using System.Collections.Generic;
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
        public ActionResult Index(BuildSet buildSet = BuildSet.Random)
        {
            if (buildSet == BuildSet.Random)
            {
                buildSet = (BuildSet)new Random(Guid.NewGuid().GetHashCode()).Next(1, Enum.GetNames(typeof(BuildSet)).Length);

                Boards randomBoard = _domain.SelectBoard();

                if (_thisBoard != randomBoard)
                {
                    return RedirectToAction("Index", randomBoard.GetDescription());
                }
            }

            var builds =
                _domain.GetBuilds(Server.MapPath(string.Format(@"~/App_Data/BuildSets/{0}.xml", buildSet.GetDescription())));

            var vm = new ViewModels.BuildStatus
            {
                BuildSet = buildSet,
                Builds = builds,
                IsRedAlertEnabled = _domain.GetRedAlertSettings(Server.MapPath(@"~/App_Data/RedAlert.xml")).IsEnabled
            };

            return View(vm);
        }

        // GET: Build
        [Route("Builds/Status/{buildId?}")]
        public JsonResult GetStatus(IEnumerable<int> buildTypeIds)
        {
            var buildStatus = _domain.GetBuildStatus(buildTypeIds);

            return Json(buildStatus, JsonRequestBehavior.AllowGet);
        }
    }
}