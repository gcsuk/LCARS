using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Xml.Linq;
using LCARS.Models.Deployments;

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

            return JsonConvert.DeserializeObject<Models.Deployments.Deployments>(jsonData);
        }

        public IEnumerable<Environment> GetEnvironmentPreferences(string fileName)
        {
            var doc = XDocument.Load(fileName);

            return doc.Root.Elements("Deployment").Select(env => new Environment
            {
                Id = env.Attribute("Id").Value,
                Name = env.Value,
                OrderId = System.Convert.ToInt32(env.Attribute("OrderId").Value)
            }).ToList();
        }

        private static string DownloadJson(string url, string apiKey)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Set("X-Octopus-ApiKey", apiKey);

                return webClient.DownloadString(url);
            }
        }
    }
}