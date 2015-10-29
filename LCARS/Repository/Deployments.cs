using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;

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

        public IEnumerable<Models.Deployments.Environment> GetEnvironmentPreferences(string fileName)
        {
            var doc = XDocument.Load(fileName);

            return doc.Root.Elements("Deployment").Select(env => new Models.Deployments.Environment
            {
                Id = env.Attribute("Id").Value,
                Name = env.Value,
                OrderId = System.Convert.ToInt32(env.Attribute("OrderId").Value)
            }).ToList();
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