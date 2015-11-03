using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LCARS.Domain;
using LCARS.Models;

namespace LCARS.Controllers
{
    public class BuildsController : Controller
    {
        private readonly IRedAlert _commonDomain;
        private readonly IBuilds _buildsDomain;
        private readonly ViewModels.Boards _thisBoard;

        public BuildsController(IRedAlert commonDomain, IBuilds buildsDomain)
        {
            _commonDomain = commonDomain;
            _buildsDomain = buildsDomain;
            _thisBoard = ViewModels.Boards.Build;
        }

        // GET: Build
        [Route("Builds/{buildSet?}")]
        public ActionResult Index(BuildSet buildSet = BuildSet.Random)
        {
            if (buildSet == BuildSet.Random)
            {
                var randomBoard = Domain.Settings.SelectBoard();

                if (_thisBoard != randomBoard)
                {
                    return RedirectToAction("Index", randomBoard.GetDescription());
                }

                buildSet = (BuildSet)new Random(Guid.NewGuid().GetHashCode()).Next(1, Enum.GetNames(typeof(BuildSet)).Length);
            }

            var builds =
                _buildsDomain.GetBuilds(Server.MapPath(string.Format(@"~/App_Data/BuildSets/{0}.json", buildSet.GetDescription())));

            var vm = new ViewModels.BuildStatus
            {
                BuildSet = buildSet,
                Builds = builds,
                IsRedAlertEnabled = _commonDomain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json")).IsEnabled
            };

            return View(vm.Builds.Count() > 8 ? "HighCount" : "LowCount", vm);
        }

        // GET: Build
        [Route("Builds/Status/{buildId?}")]
        public JsonResult GetStatus(IEnumerable<int> buildTypeIds)
        {
            try
            {
                var buildStatus = _buildsDomain.GetBuildStatus(buildTypeIds);

                return Json(buildStatus, JsonRequestBehavior.AllowGet);
            }
            catch (System.Net.WebException)
            {
                var builds = buildTypeIds.Select(buildTypeId => new Build
                {
                    TypeId = buildTypeId
                }).ToList();

                return Json(builds, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}