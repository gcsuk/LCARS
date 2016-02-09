using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using LCARS.Models.Builds;

namespace LCARS.Services
{
    public class Builds : IBuilds
    {
        private readonly string _domain;
        private readonly string _username;
        private readonly string _password;

        public Builds(string domain, string username, string password)
        {
            _domain = domain;
            _username = username;
            _password = password;
        }

        public Dictionary<string, int> GetBuildsRunning()
        {
            var doc = GetXml($"http://{_domain}/guestAuth/app/rest/builds?locator=running:true");

            var builds = new Dictionary<string, int>();

            if (doc == null)
            {
                return builds;
            }

            foreach (
                var build in
                    doc.Root.Elements("build").Where(build => !builds.ContainsKey(build.Attribute("buildTypeId").Value))
                )
            {
                builds.Add(build.Attribute("buildTypeId").Value, Convert.ToInt32(build.Attribute("id").Value));
            }

            return builds;
        }

        public BuildProgress GetBuildProgress(int buildId)
        {
            var doc = GetXml($"http://user:pwd@{_domain}/guestAuth/app/rest/builds/id:{buildId}");

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

        public KeyValuePair<string, string> GetLastBuildStatus(string buildTypeId)
        {
            var doc = GetXml($"http://{_domain}/guestAuth/app/rest/builds?locator=buildType:(id:{buildTypeId})");

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

            var nc = new NetworkCredential(_username, _password);
            var cCache = new CredentialCache { { requestUri, "Basic", nc } };

            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            request.Credentials = cCache;
            request.PreAuthenticate = true;
            request.Method = WebRequestMethods.Http.Get;
            request.Timeout = 2000;

            using (var response = (HttpWebResponse) request.GetResponse())
            {
                var doc = XDocument.Load(new StreamReader(response.GetResponseStream()));

                return doc.Root == null ? null : doc;
            }
        }
    }
}