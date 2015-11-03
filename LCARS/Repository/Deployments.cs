using Newtonsoft.Json;
using System.Net;

namespace LCARS.Repository
{
    public class Deployments : IDeployments
    {
        private readonly string _deploymentServer;
        private readonly string _apiKey;

        public Deployments(string deploymentServer, string apiKey)
        {
            _deploymentServer = deploymentServer;
            _apiKey = apiKey;
        }

        public Models.Deployments.Deployments Get()
        {
            var jsonData = "";

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Set("X-Octopus-ApiKey", _apiKey);

                    jsonData = webClient.DownloadString(_deploymentServer);
                }
            }
            catch
            {
                // jsonData will be empty
            }

            return !string.IsNullOrEmpty(jsonData)
                ? JsonConvert.DeserializeObject<Models.Deployments.Deployments>(jsonData)
                : new Models.Deployments.Deployments();
        }
    }
}