using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using LCARS.Domain;
using LCARS.Models.Builds;

namespace LCARS.Controllers
{
    public class BuildsController : Controller
    {
        private readonly IRedAlert _commonDomain;
        private readonly IBuilds _buildsDomain;

        public BuildsController(IRedAlert commonDomain, IBuilds buildsDomain)
        {
            _commonDomain = commonDomain;
            _buildsDomain = buildsDomain;
        }

        // GET: Build
        [Route("Builds/{typeId?}")]
        public ActionResult Index(int typeId)
        {
            var buildProjects = _buildsDomain.GetBuilds(Server.MapPath("~/App_Data/Builds.json")).ToList();

            if (typeId == 0)
            {
                typeId = new Random(Guid.NewGuid().GetHashCode()).Next(1, buildProjects.Count);
            }

            var builds = buildProjects.Single(b => b.Id == typeId).Builds;

            var vm = new ViewModels.BuildStatus
            {
                ProjectId = typeId,
                Builds = builds,
                IsRedAlertEnabled = _commonDomain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json")).IsEnabled
            };

            return View(vm.Builds.Count() > 8 ? "HighCount" : "LowCount", vm);
        }

        // GET: Build
        [Route("Builds/Status/{buildId?}")]
        public JsonResult GetStatus(IEnumerable<string> buildTypeIds)
        {
            try
            {
                var buildStatus = _buildsDomain.GetBuildStatus(buildTypeIds);

                return Json(buildStatus, JsonRequestBehavior.AllowGet);
            }
            catch (System.Net.WebException ex)
            {
                Debug.WriteLine(ex.Message);

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