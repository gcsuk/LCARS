using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using LCARS.Domain;

namespace LCARS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IScreens _screensDomain;

        public HomeController(IScreens screensDomain)
        {
            _screensDomain = screensDomain;
        }

        public ActionResult Index(int screenId = 0)
        {
            if (screenId == 0 && Session["ScreenId"] != null)
            {
                screenId = (int)Session["ScreenId"];
            }

            if (screenId == 0)
            {
                var randomBoard = Settings.SelectBoard();

                return RedirectToAction("Index", randomBoard.GetDescription());
            }

            var screen =
                _screensDomain.GetScreens(Server.MapPath(@"~/App_Data/Screens.json"))
                    .SingleOrDefault(s => screenId == s.Id);

            if (screen == null || screen.Boards == null)
            {
                var randomBoard = Settings.SelectBoard();

                return RedirectToAction("Index", randomBoard.GetDescription());
            }

            Session["ScreenId"] = screenId;

            var boards = screen.Boards;

            var randomiser = new Random();

            var selectedBoardIndex = randomiser.Next(0, boards.Count);

            var selectedBoard = boards[selectedBoardIndex].CategoryId.GetDescription();

            var argument = "";

            if (!string.IsNullOrEmpty(boards[selectedBoardIndex].Argument))
            {
                argument = boards[selectedBoardIndex].Argument;
            }

            return string.IsNullOrEmpty(argument)
                ? RedirectToAction("Index", selectedBoard)
                : RedirectToAction("Index", new RouteValueDictionary(new {controller = selectedBoard, action = "Index", typeId = argument}));
        }
    }
}