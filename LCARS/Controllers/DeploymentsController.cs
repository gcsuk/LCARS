using System.Linq;
using System.Web.Mvc;
using LCARS.Domain;
using LCARS.ViewModels.Deployments;

namespace LCARS.Controllers
{
    public class DeploymentsController : Controller
    {
        private readonly IDeployments _domain;

        public DeploymentsController(IDeployments domain)
        {
            _domain = domain;
        }

        // GET: Deployments
        public ActionResult Index()
        {
            var deployments = _domain.Get().OrderBy(g => g.ProjectGroup).ThenBy(p => p.Project).ToList();

            var projects =
                deployments.GroupBy(p => p.ProjectId)
                    .Select(
                        g =>
                            new Project
                            {
                                Id = g.First().ProjectId,
                                Name = g.First().Project.Replace("CasinoToolkit_", "")
                            })
                    .ToList();

            var environments =
                deployments.GroupBy(p => p.EnvironmentId)
                    .Select(
                        g =>
                            new Environment
                            {
                                Id = g.First().EnvironmentId,
                                Name = g.First().Environment
                            })
                    .ToList();

            environments =
                _domain.SetEnvironmentOrder(environments, Server.MapPath(@"~/App_Data/Deployments.json"))
                    .ToList();

            var vm = new DeploymentStatus
            {
                Projects = projects,
                Environments = environments,
                Deployments = deployments
            };

            return View(vm);
        }

        [HttpGet]
        public JsonResult GetStatus()
        {
            return Json(_domain.Get().ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}