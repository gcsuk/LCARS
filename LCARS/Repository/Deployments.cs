using System.Collections.Generic;
using System.IO;
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
            var jsonData = DownloadJson(_deploymentServer, _apiKey);

            return !string.IsNullOrEmpty(jsonData)
                ? JsonConvert.DeserializeObject<Models.Deployments.Deployments>(jsonData)
                : new Models.Deployments.Deployments();
        }

        public IEnumerable<Models.Deployments.Environment> GetEnvironmentPreferences(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new IOException("RedAlert file does not exist. Refer to ReadMe file for setup instructions.");
            }

            return JsonConvert.DeserializeObject<IEnumerable<Models.Deployments.Environment>>(File.ReadAllText(filePath));
        }

        private static string DownloadJson(string url, string apiKey)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Set("X-Octopus-ApiKey", apiKey);

                    return webClient.DownloadString(url);
                }
            }
            catch
            {
                return "";
            }
        }
    }
}