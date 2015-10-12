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

        public Deployments(string deploymentServer)
        {
            _deploymentServer = deploymentServer;
        }

        public Models.Deployments.Deployments Get()
        {
            var jsonData = DownloadJson(_deploymentServer);

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

        private static string DownloadJson(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Set("X-Octopus-ApiKey", "API-3KKXFTSGULC9UXF7UFVPOQPL0C");

                return webClient.DownloadString(url);
            }
        }
    }
}