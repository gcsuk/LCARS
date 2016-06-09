using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using System;

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
                    var lineComments = _domain.GetComments(
                        settings.BaseUrl.Replace("OWNER", settings.Owner).Replace("REPOSITORY", repository) +
                        "/pulls", repository, pr.Number).ToList();

                    var mainComments = _domain.GetComments(
                            settings.BaseUrl.Replace("OWNER", settings.Owner).Replace("REPOSITORY", repository) +
                            "/issues", repository, pr.Number).ToList();

                    lineComments.AddRange(mainComments);

                    pr.Comments = lineComments;
                });

                vm.Add(repo);
            }

            return View(vm);
        }

        public ActionResult Shipped()
        {
            var vm = new List<ViewModels.GitHub.ShippedPullRequest>();

            var settings = _domain.GetSettings(Server.MapPath(@"~/App_Data/GitHub.json"));

            foreach (var repository in settings.Repositories)
            {
                var pullRequests = _domain.GetPullRequests(settings.BaseUrl.Replace("OWNER", settings.Owner) + "/issues", repository).ToList();

                pullRequests.ForEach(pr =>
                {
                    var shipComment = pr.Comments.FirstOrDefault(c => c.Body.Contains(":ship") || c.Body.Contains(":sheep"));

                    if (shipComment != null)
                    {
                        vm.Add(new ViewModels.GitHub.ShippedPullRequest
                        {
                            AuthorName = pr.AuthorName,
                            Number = pr.Number,
                            Title = pr.Title.Replace("â€¦", "..."),
                            ShippedBy = shipComment.User.Name,
                            ShippedOn = shipComment.DateCreated
                        });
                    }
                });
            }

            if (vm.Any())
            {
                var randomSelectionIndex = new Random().Next(vm.Count);

                return View(vm[randomSelectionIndex]);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Alert()
        {
            return View();
        }
    }
}