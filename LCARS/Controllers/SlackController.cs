using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace LCARS.Controllers
{
    public class SlackController : ApiController
    {
        private readonly Domain.IRedAlert _domain;

        public SlackController(Domain.IRedAlert domain)
        {
            _domain = domain;
        }

        public HttpResponseMessage Post([FromBody]ViewModels.Slack.SlackData data)
        {
            if (data == null || string.IsNullOrEmpty(data.Text))
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        text =
                            "You need to set the Red Alert properly! Use \"RedAlert alert-type end-date\" (in the format dd/mm/yyyy hh:mm). Leave date blank for next Friday at 16:30."
                    });
            }

            // Remove the trigger word, otherwise it will be part of the response and trigger an infinite alert loop
            data.Text = data.Text.Replace(data.Trigger_Word, "").Trim();

            var parameters = data.Text.Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries);

            var settings = new ViewModels.RedAlert
            {
                IsEnabled = true,
                AlertType = parameters.Length == 0 ? "Beer" : parameters[0].Trim()
            };

            var targetDate = new DateTime();

            var defaultDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 16, 30, 0);

            if (parameters.Length < 2)
            {
                targetDate = defaultDate;
            }
            else if (!DateTime.TryParse(parameters[1], out targetDate)) // If this succeeds, target date will be set by the TryParse
            {
                targetDate = defaultDate;
            }

            settings.TargetDate = targetDate;

            try
            {
                _domain.UpdateRedAlert(HttpContext.Current.Server.MapPath("../" + ConfigurationManager.AppSettings["RedAlertSettingsPath"]), settings);

#if DEBUG
                File.WriteAllText(HttpContext.Current.Server.MapPath("temp.json"), JsonConvert.SerializeObject(data));
#endif
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { text = settings.AlertType + " Alert Activated"});
        }
    }
}