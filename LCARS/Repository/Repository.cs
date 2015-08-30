using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using LCARS.Models;

namespace LCARS.Services
{
    public class Repository : IRepository
    {
        private readonly string _username;
        private readonly string _password;

        public Repository(string username, string password)
        {
            _username = username;
            _password = password;
        }

        //private const string Username = "rob.king";
        //private const string Password = "#(4t5vIe=r~3&<s";

        public IEnumerable<Tenant> GetStatus(string fileName)
        {
            XDocument doc = XDocument.Load(fileName);

            if (doc.Root == null)
                return null;

            List<Tenant> tenants = new List<Tenant>();

            foreach (var tenantData in doc.Root.Elements("Tenant"))
            {
                var tenant = new Tenant
                {
                    Id = Convert.ToInt32(tenantData.Attribute("ID").Value),
                    Name = tenantData.Attribute("Name").Value
                };

                foreach (var dependencyData in tenantData.Element("Dependencies").Elements("Dependency"))
                {
                    var dependency = new Dependency { Name = dependencyData.Attribute("Name").Value };

                    foreach (var environmentData in dependencyData.Element("Environments").Elements("Environment"))
                    {
                        dependency.Environments.Add(new Models.Environment
                        {
                            Name = environmentData.Attribute("Name").Value,
                            Status = environmentData.Attribute("Status").Value
                        });
                    }

                    tenant.Dependencies.Add(dependency);
                }

                tenants.Add(tenant);
            }

            return tenants;
        }

        public void UpdateStatus(string fileName, string tenant, string dependency, string environment, string status)
        {
            XDocument doc = XDocument.Load(fileName);

            if (doc.Root == null)
                return;

            var tenantElement = doc.Root.Elements("Tenant").Single(t => t.Attribute("Name").Value == tenant);

            var dependencyElement =
                tenantElement.Element("Dependencies")
                    .Elements("Dependency")
                    .Single(d => d.Attribute("Name").Value == dependency);

            var environmentElement =
                dependencyElement.Element("Environments")
                    .Elements("Environment")
                    .Single(e => e.Attribute("Name").Value == environment);

            environmentElement.Attribute("Status").Value = status;

            doc.Save(fileName);
        }

        public RedAlert GetRedAlertSettings(string fileName)
        {
            XDocument doc = XDocument.Load(fileName);

            if (doc.Root == null)
                return null;

            return new RedAlert
            {
                IsEnabled = doc.Root.Element("IsEnabled").Value == "1",
                TargetDate = doc.Root.Element("TargetDate").Value,
            };
        }

        public void UpdateRedAlertSettings(string fileName, bool isEnabled, string targetDate)
        {
            XDocument doc = XDocument.Load(fileName);

            if (doc.Root == null)
                return;

            doc.Root.Element("IsEnabled").Value = (isEnabled ? "1" : "0");
            doc.Root.Element("TargetDate").Value = targetDate;

            doc.Save(fileName);
        }

        public IEnumerable<Build> GetBuilds(string fileName)
        {
            var doc = XDocument.Load(fileName);

            return doc.Root.Elements("Build").Select(build => new Build
            {
                TypeId = Convert.ToInt32(build.Attribute("TypeId").Value),
                Name = build.Attribute("Name").Value
            }).ToList();
        }

        public Dictionary<int, int> GetBuildsRunning()
        {
            var doc = GetXml("http://teamcity.bedegaming.com/httpAuth/app/rest/builds?locator=running:true");

            var builds = new Dictionary<int, int>();

            if (doc == null)
            {
                return builds;
            }

            foreach (var build in doc.Root.Elements("build"))
            {
                builds.Add(Convert.ToInt32(build.Attribute("buildTypeId").Value.Replace("bt", "")),
                    Convert.ToInt32(build.Attribute("id").Value));
            }

            return builds;
        }

        public BuildProgress GetBuildProgress(int buildId)
        {
            var doc = GetXml("http://user:pwd@teamcity.bedegaming.com/httpAuth/app/rest/builds/id:" + buildId);

            if (doc == null)
            {
                return new BuildProgress
                {
                    Percentage = 0,
                    Status = ""
                };
            }

            var runningInfo = doc.Root.Element("running-info");

            if (runningInfo == null)
            {
                return new BuildProgress
                {
                    Status = doc.Root.Element("statusText").Value,
                    Percentage = 100
                };
            }

            return new BuildProgress
            {
                Status = runningInfo.Attribute("currentStageText").Value,
                Percentage = Convert.ToInt32(runningInfo.Attribute("percentageComplete").Value)
            };
        }

        public KeyValuePair<string, string> GetLastBuildStatus(int buildTypeId)
        {
            var doc = GetXml(string.Format("http://teamcity.bedegaming.com/httpAuth/app/rest/builds?locator=buildType:(id:bt{0})", buildTypeId));

            if (doc == null)
            {
                return new KeyValuePair<string, string>("Unknown", "Unknown");
            }

            return new KeyValuePair<string, string>(doc.Root.Element("build").Attribute("status").Value,
                doc.Root.Element("build").Attribute("number").Value);
        }

        private XDocument GetXml(string url)
        {
            Uri requestUri;
            Uri.TryCreate(url, UriKind.Absolute, out requestUri);

            NetworkCredential nc = new NetworkCredential(_username, _password);
            CredentialCache cCache = new CredentialCache { { requestUri, "Basic", nc } };

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

            request.Credentials = cCache;
            request.PreAuthenticate = true;
            request.Method = WebRequestMethods.Http.Get;

            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                var doc = XDocument.Load(new StreamReader(response.GetResponseStream()));

                return doc.Root == null ? null : doc;
            }
        }
    }
}