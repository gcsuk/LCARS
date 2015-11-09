using System;
using System.Web.Mvc;
using LCARS.ViewModels;
using LCARS.Domain;

namespace LCARS.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRedAlert _redAlertDomain;
        private readonly ISettings _settingsDomain;

        public AdminController(IRedAlert redAlertDomain, ISettings settingsDomain)
        {
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
                    TempData["menuColor"] = "red";
                    return View("Screens");
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
                    return View();
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

        [HttpPost, Route("Admin/UpdateRedAlert")]
        public bool UpdateRedAlert(bool isEnabled, string targetDate, string alertType)
        {
            try
            {
                var settings = new ViewModels.RedAlert
                {
                    IsEnabled = isEnabled,
                    TargetDate = targetDate == "" ? (DateTime?) null : Convert.ToDateTime(targetDate),
                    AlertType = alertType
                };

                _redAlertDomain.UpdateRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json"), settings);

                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}