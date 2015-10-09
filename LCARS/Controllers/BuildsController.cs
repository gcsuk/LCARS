using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LCARS.Domain;
using LCARS.Models;

namespace LCARS.Controllers
{
    public class BuildsController : Controller
    {
        private readonly ICommon _commonDomain;
        private readonly IBuilds _buildsDomain;
        private readonly Boards _thisBoard;

        public BuildsController(ICommon commonDomain, IBuilds buildsDomain)
        {
            _commonDomain = commonDomain;
            _buildsDomain = buildsDomain;
            _thisBoard = Boards.Build;
        }

        // GET: Build
        [Route("Builds/{buildSet?}")]
        public ActionResult Index(BuildSet buildSet = BuildSet.Random)
        {
            if (buildSet == BuildSet.Random)
            {
                buildSet = (BuildSet)new Random(Guid.NewGuid().GetHashCode()).Next(1, Enum.GetNames(typeof(BuildSet)).Length);

                Boards randomBoard = _commonDomain.SelectBoard();

                if (_thisBoard != randomBoard)
                {
                    return RedirectToAction("Index", randomBoard.GetDescription());
                }
            }

            var builds =
                _buildsDomain.GetBuilds(Server.MapPath(string.Format(@"~/App_Data/BuildSets/{0}.xml", buildSet.GetDescription())));

            var vm = new ViewModels.BuildStatus
            {
                BuildSet = buildSet,
                Builds = builds,
                IsRedAlertEnabled = _commonDomain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.xml")).IsEnabled
            };

            return View(vm);
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
                var builds = new List<Build>();

                foreach (int buildTypeId in buildTypeIds)
                {
                    builds.Add(new Build { TypeId = buildTypeId });
                }

                return Json(builds, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}