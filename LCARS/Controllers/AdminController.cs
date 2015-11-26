using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LCARS.ViewModels;
using LCARS.Domain;

namespace LCARS.Controllers
{
    public class AdminController : Controller
    {
        private readonly IScreens _screensDomain;
        private readonly IIssues _issuesDomain;
        private readonly IRedAlert _redAlertDomain;
        private readonly ISettings _settingsDomain;

        public AdminController(IScreens screensDomain, IIssues issuesDomain, IRedAlert redAlertDomain, ISettings settingsDomain)
        {
            _screensDomain = screensDomain;
            _issuesDomain = issuesDomain;
            _redAlertDomain = redAlertDomain;
            _settingsDomain = settingsDomain;
        }

        // GET: Admin
        [Route("Admin/{selectedMenu?}")]
        public ActionResult Index(AdminMenu selectedMenu = AdminMenu.Screens)
        {
            TempData["menuId"] = selectedMenu;

            switch (selectedMenu)
            {
                case AdminMenu.Screens:
                    TempData["menuColor"] = "blue";

                    var screensVm = new ViewModels.Screens.Screens
                    {
                        Categories = GetBoards(),
                        ScreenList = _screensDomain.GetScreens(Server.MapPath(@"~/App_Data/Screens.json")).ToList()
                    };

                    return View("Screens", screensVm);
                case AdminMenu.Environments:
                    TempData["menuColor"] = "red";
                    return View("Environments");
                case AdminMenu.Builds:
                    TempData["menuColor"] = "blue";
                    return View();
                case AdminMenu.Deployments:
                    TempData["menuColor"] = "apricot";
                    return View();
                case AdminMenu.Issues:
                    TempData["menuColor"] = "red";

                    var issuesVm = _issuesDomain.GetQueries(Server.MapPath(@"~/App_Data/Issues.json"));

                    return View("Issues", issuesVm);
                case AdminMenu.RedAlert:
                    TempData["menuColor"] = "blue";

                    var redAlertDetails = _redAlertDomain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json"));

                    var redAlertVm = new ViewModels.Admin.RedAlert
                    {
                        IsEnabled = redAlertDetails.IsEnabled,
                        AlertType = redAlertDetails.AlertType,
                        TargetYear = Convert.ToDateTime(redAlertDetails.TargetDate).Year,
                        TargetMonth = Convert.ToDateTime(redAlertDetails.TargetDate).Month,
                        TargetDay = Convert.ToDateTime(redAlertDetails.TargetDate).Day,
                        TargetHour = Convert.ToDateTime(redAlertDetails.TargetDate).Hour,
                        TargetMinute = Convert.ToDateTime(redAlertDetails.TargetDate).Minute
                    };

                    return View("RedAlert", redAlertVm);
                case AdminMenu.Settings:
                    TempData["menuColor"] = "apricot";

                    var settingsVm = _settingsDomain.GetSettings(Server.MapPath(@"~/App_Data/Settings.json"));

                    return View("Settings", settingsVm);
                default:
                    throw new ArgumentOutOfRangeException(nameof(selectedMenu), selectedMenu, null);
            }
        }

        [Route("Admin/GetScreen/{id}")]
        public JsonResult GetScreen(int id = 0)
        {
            switch (id)
            {
                case -1:
                    return Json(new ViewModels.Screens.Screen(), JsonRequestBehavior.AllowGet);
                case 0:
                    return Json(_screensDomain.GetScreens(Server.MapPath(@"~/App_Data/Screens.json")).First(),
                        JsonRequestBehavior.AllowGet);
                default:
                    return Json(_screensDomain.GetScreens(Server.MapPath(@"~/App_Data/Screens.json")).Single(i => i.Id == id),
                        JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, Route("Admin/UpdateScreen")]
        public ActionResult UpdateScreen(ViewModels.Screens.Screen screen)
        {
            if (screen.Id <= 0)
            {
                throw new ArgumentException("Invalid ID. Try again.", "screen");
            }

            return Json(_screensDomain.UpdateScreen(Server.MapPath(@"~/App_Data/Screens.json"), screen), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, Route("Admin/DeleteScreen")]
        public ActionResult DeleteScreen(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("Invalid ID. Try again.", "id");
                }

                _screensDomain.DeleteScreen(Server.MapPath(@"~/App_Data/Screens.json"), id);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, Route("Admin/AddBoard")]
        public ActionResult AddBoard(ViewModels.Screens.Board board)
        {
            _screensDomain.AddBoard(Server.MapPath(@"~/App_Data/Screens.json"), board);

            return Json(board.ScreenId, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, Route("Admin/DeleteBoard")]
        public ActionResult DeleteBoard(int screenId, string boardId)
        {
            _screensDomain.DeleteBoard(Server.MapPath(@"~/App_Data/Screens.json"), screenId, boardId);

            return Json(screenId, JsonRequestBehavior.AllowGet);
        }

        [Route("Admin/GetIssueQuery/{id}")]
        public JsonResult GetIssueQuery(int id = 0)
        {
            switch (id)
            {
                case -1:
                    return Json(new ViewModels.Issues.Query(), JsonRequestBehavior.AllowGet);
                case 0:
                    return Json(_issuesDomain.GetQueries(Server.MapPath(@"~/App_Data/Issues.json")).First(),
                        JsonRequestBehavior.AllowGet);
                default:
                    return Json(_issuesDomain.GetQueries(Server.MapPath(@"~/App_Data/Issues.json")).Single(i => i.Id == id),
                        JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, Route("Admin/UpdateIssueQuery")]
        public ActionResult UpdateIssueQuery(ViewModels.Issues.Query query)
        {
            if (query.Id <= 0)
            {
                throw new ArgumentException("Invalid ID. Try again.", "query");
            }

            return Json(_issuesDomain.UpdateQuery(Server.MapPath(@"~/App_Data/Issues.json"), query), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, Route("Admin/DeleteIssueQuery")]
        public ActionResult DeleteIssueQuery(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("Invalid ID. Try again.", "id");
                }

                _issuesDomain.DeleteQuery(Server.MapPath(@"~/App_Data/Issues.json"), id);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, Route("Admin/UpdateRedAlert")]
        public JsonResult UpdateRedAlert(bool isEnabled, string targetDate, string alertType)
        {
            try
            {
                var settings = new ViewModels.RedAlert
                {
                    IsEnabled = isEnabled,
                    TargetDate = isEnabled ? Convert.ToDateTime(targetDate) : (DateTime?) null,
                    AlertType = alertType
                };

                _redAlertDomain.UpdateRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json"), settings);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost, Route("Admin/Settings"), ValidateInput(false)]
        public ActionResult Settings(ViewModels.Settings viewModel)
        {
            try
            {
                _settingsDomain.UpdateSettings(Server.MapPath(@"~/App_Data/Settings.json"), viewModel);

                TempData["ShowConfirmation"] = "display: block";
            }
            catch
            {
                TempData["ShowError"] = "display: block";
            }

            return RedirectToAction("Settings");
        }

        private Dictionary<int, string> GetBoards()
        {
            return Enum.GetValues(typeof(Boards))
                .Cast<object>()
                .ToDictionary(val => (int)val, val => Enum.GetName(typeof(Boards), val));
        }
    }
}