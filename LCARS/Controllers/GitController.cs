using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace LCARS.Controllers
{
    public class GitController : Controller
    {
        private readonly Domain.IGitHub _domain;

        public GitController(Domain.IGitHub domain)
        {
            _domain = domain;
        }

        // GET: Git
        public ActionResult Index()
        {
            var vm = new List<ViewModels.GitHub.Repository>();

            var settings = _domain.GetSettings(Server.MapPath(@"~/App_Data/GitHub.json"));

            foreach (var repository in settings.Repositories)
            {
                var repo = new ViewModels.GitHub.Repository
                {
                    Name = repository,
                    Branches =
                        _domain.GetBranches(
                            settings.BaseUrl.Replace("OWNER", settings.Owner).Replace("REPOSITORY", repository) + "/branches",
                            repository).ToList(),
                    PullRequests = 
                        _domain.GetPullRequests(
                            settings.BaseUrl.Replace("OWNER", settings.Owner).Replace("REPOSITORY", repository) + "/pulls",
                            repository).ToList()
                };

                repo.PullRequests.ForEach(pr =>
                {
                    pr.Comments = _domain.GetComments(
                        settings.BaseUrl.Replace("OWNER", settings.Owner).Replace("REPOSITORY", repository) +
                        "/pulls/" + pr.Number + "/comments",
                        repository).ToList();

                    if (pr.Comments == null || !pr.Comments.Any())
                    {
                        pr.Comments = _domain.GetComments(
                            settings.BaseUrl.Replace("OWNER", settings.Owner).Replace("REPOSITORY", repository) +
                            "/issues/" + pr.Number + "/comments",
                            repository).ToList();
                    }
                });

                vm.Add(repo);
            }

            return View(vm);
        }

        public ActionResult Alert()
        {
            return View();
        }
    }
}