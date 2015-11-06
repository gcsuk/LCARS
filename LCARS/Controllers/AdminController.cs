using System;
using System.Web.Mvc;
using LCARS.ViewModels;
using LCARS.Domain;

namespace LCARS.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRedAlert _redAlertDomain;

        public AdminController(IRedAlert redAlertDomain)
        {
            _redAlertDomain = redAlertDomain;
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

                    var vm = new ViewModels.Admin.RedAlert
                    {
                        IsEnabled = redAlertDetails.IsEnabled,
                        AlertType = redAlertDetails.AlertType,
                        TargetYear = Convert.ToDateTime(redAlertDetails.TargetDate).Year,
                        TargetMonth = Convert.ToDateTime(redAlertDetails.TargetDate).Month,
                        TargetDay = Convert.ToDateTime(redAlertDetails.TargetDate).Day,
                        TargetHour = Convert.ToDateTime(redAlertDetails.TargetDate).Hour,
                        TargetMinute = Convert.ToDateTime(redAlertDetails.TargetDate).Minute
                    };

                    return View("RedAlert", vm);
                case AdminMenu.Settings:
                    TempData["menuColor"] = "apricot";
                    return View();
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