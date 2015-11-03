using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using LCARS.Models;

namespace LCARS.Repository
{
    public class Builds : IBuilds
    {
        private readonly string _username;
        private readonly string _password;

        public Builds(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public Dictionary<int, int> GetBuildsRunning()
        {
            var doc = GetXml("http://teamcity.bedegaming.com/httpAuth/app/rest/builds?locator=running:true");

            var builds = new Dictionary<int, int>();

            if (doc == null)
            {
                return builds;
            }

            foreach (
                var build in
                    doc.Root.Elements("build")
                        .Where(
                            build =>
                                !builds.ContainsKey(
                                    Convert.ToInt32(build.Attribute("buildTypeId").Value.Replace("bt", "")))))
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
            request.Timeout = 2000;

            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                var doc = XDocument.Load(new StreamReader(response.GetResponseStream()));

                return doc.Root == null ? null : doc;
            }
        }
    }
}