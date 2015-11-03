﻿using System.Web.Mvc;
using LCARS.Domain;
using LCARS.ViewModels;

namespace LCARS.Controllers
{
	public class EnvironmentsController : Controller
	{
        private readonly IRedAlert _commonDomain;
		private readonly IEnvironments _environmentsDomain;
        private readonly Boards _thisBoard;

        public EnvironmentsController(IRedAlert commonDomain, IEnvironments environmentsDomain)
        {
            _commonDomain = commonDomain;
            _environmentsDomain = environmentsDomain;
            _thisBoard = Boards.Environment;
        }

		public ActionResult Index()
		{
            var randomBoard = Domain.Settings.SelectBoard();

            if (_thisBoard != randomBoard)
            {
                return RedirectToAction("Index", randomBoard.GetDescription());
            }

            var vm = new ViewModels.Environments.Environments
            {
                Tenants = _environmentsDomain.Get(Server.MapPath(@"~/App_Data/Environments.json")),
                IsRedAlertEnabled = _commonDomain.GetRedAlert(Server.MapPath(@"~/App_Data/RedAlert.json")).IsEnabled
            };

            return View(vm);
        }

        [HttpPost]
        public void UpdateStatus(string tenant, string environment, string currentStatus)
        {
            _environmentsDomain.Update(Server.MapPath(@"~/App_Data/Environments.json"), tenant, environment, currentStatus);
        }
	}
}