using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace LCARS.Services
{
    public class GitHub<T> : IGitHub<T> where T : class
    {
        private readonly string _username;
        private readonly string _password;

        public GitHub(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public IEnumerable<T> Get(string url, string repository)
        {
            using (WebClient webClient = new WebClient())
            {
                var items = new List<T>();
                var hasRecords = true;
                var pageNumber = 1;
                var pagedUrl = url + "?per_page=100&page=1";

                while (hasRecords)
                {
                    webClient.Headers.Set("Authorization", "Basic " + Common.GetEncodedCredentials(_username, _password));
                    webClient.Headers.Set("User-Agent", repository);

                    var jsonData = webClient.DownloadString(pagedUrl);

                    var newItems = JsonConvert.DeserializeObject<List<T>>(jsonData);

                    if (newItems.Any())
                    {
                        items.AddRange(newItems);

                        pageNumber++;
                        pagedUrl = url + "?per_page=100&page=" + pageNumber;
                    }
                    else
                    {
                        hasRecords = false;
                    }
                }

                return items;
            }
        }

        public int GetCount(string url, string repository)
        {
            using (WebClient webClient = new WebClient())
            {
                var count = 0;
                var hasRecords = true;
                var pageNumber = 1;
                var pagedUrl = url + "?per_page=100&page=1";

                while (hasRecords)
                {
                    webClient.Headers.Set("Authorization", "Basic " + Common.GetEncodedCredentials(_username, _password));
                    webClient.Headers.Set("User-Agent", repository);

                    var jsonData = webClient.DownloadString(pagedUrl);

                    var newItems = JsonConvert.DeserializeObject<List<T>>(jsonData);

                    if (newItems.Any())
                    {
                        count += newItems.Count;

                        pageNumber++;
                        pagedUrl = url + "?per_page=100&page=" + pageNumber;
                    }
                    else
                    {
                        hasRecords = false;
                    }
                }

                return count;
            }
        }
    }
}