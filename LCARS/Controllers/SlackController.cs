using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Http;

namespace LCARS.Controllers
{
    public class SlackController : ApiController
    {
        private readonly Domain.IRedAlert _domain;

        public SlackController(Domain.IRedAlert domain)
        {
            _domain = domain;
        }

        public string Post([FromBody]ViewModels.Slack.SlackData data)
        {
            if (data == null || string.IsNullOrEmpty(data.Text))
            {
                return "You need to set the Red Alert properly! Use \"RedAlert alert-type end-date\" (in the format dd/mm/yyyy hh:mm). Leave date blank for next Friday at 16:30.";
            }

            string[] parameters = data.Text.Split(',');

            if (parameters.Length == 0)
            {
                return "You need to set the Red Alert properly! Use \"RedAlert alert-type end-date\" (in the format dd/mm/yyyy hh:mm). Leave date blank for next Friday at 16:30.";
            }

            var settings = new ViewModels.RedAlert { IsEnabled = true };

            if (parameters.Length >= 1)
            {
                settings.AlertType = parameters[0].Trim();
            }

            if (parameters.Length >= 2)
            {
                DateTime targetDate;

                settings.TargetDate = DateTime.TryParse(parameters[1], out targetDate) ? targetDate : new DateTime?();
            }

            File.WriteAllText(HttpContext.Current.Server.MapPath("temp.json"), settings.ToString());

            _domain.UpdateRedAlert(HttpContext.Current.Server.MapPath("../" + ConfigurationManager.AppSettings["RedAlertSettingsPath"]), settings);

            return settings.AlertType + " Alert Activated";
        }
    }
}