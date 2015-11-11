using System;
using System.Net;
using System.Text;
using LCARS.Models.Issues;
using Newtonsoft.Json;

namespace LCARS.Services
{
    public class Issues : IIssues
    {
        private readonly string _url;
        private readonly string _username;
        private readonly string _password;

        public Issues(string url, string username, string password)
        {
            _url = url;
            _username = username;
            _password = password;
        }

        public Parent Get(string query)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Set("Authorization", "Basic " + GetEncodedCredentials(_username, _password));

                var jsonData = webClient.DownloadString(_url + query + "&maxResults=1000");

                return JsonConvert.DeserializeObject<Parent>(jsonData);
            }
        }

        private static string GetEncodedCredentials(string username, string password)
        {
            var mergedCredentials = string.Format("{0}:{1}", username, password);
            var byteCredentials = Encoding.UTF8.GetBytes(mergedCredentials);
            return Convert.ToBase64String(byteCredentials);
        }
    }
}