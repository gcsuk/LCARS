using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LCARS.Models;
using LCARS.ViewModels.Issues;
using Newtonsoft.Json;
using System.Linq;
using LCARS.Repositories;

namespace LCARS.Services
{
    public class IssuesService : IIssuesService
    {
        private readonly IIssuesRepository _issuesRepository;
        private readonly Settings _settings;

        public IssuesService(IIssuesRepository issuesRepository, ISettingsService settingsService)
        {
            _issuesRepository = issuesRepository;
            _settings = settingsService.GetSettings();
        }

        public IEnumerable<Query> GetQueries(int? typeId = null)
        {
            var queries = _issuesRepository.GetAll().Where(i => i.Id == (typeId ?? i.Id));

            return queries.Select(q => new Query
            {
                Id = q.Id,
                Name = q.Name,
                Deadline = q.Deadline,
                Jql = q.Jql
            }).OrderBy(r => Guid.NewGuid());
        }

        public async Task<IEnumerable<Issue>> GetIssues(string query)
        {
            try
            {
                Models.Issues.Parent data;

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMilliseconds(1000);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_settings.IssuesUsername}:{_settings.IssuesPassword}")));

                    var jsonData = await client.GetStringAsync(_settings.IssuesUrl + query + "&maxResults=1000");

                    data = JsonConvert.DeserializeObject<Models.Issues.Parent>(jsonData);
                }

                return data.Issues.OrderByDescending(i => i.Fields.Created).Select(i => new Issue
                {
                    Id = i.Key,
                    Summary = i.Fields.Summary,
                    Status = i.Fields.Status == null ? "N/A" : i.Fields.Status.Name,
                    Reporter = i.Fields.Reporter == null ? "N/A" : i.Fields.Reporter.DisplayName,
                    Priority = i.Fields.Priority == null ? "N/A" : i.Fields.Priority.Name,
                    PriorityIcon = i.Fields.Priority == null ? "N/At" : i.Fields.Priority.IconUrl,
                    Assignee = i.Fields.Assignee == null ? "N/A" : i.Fields.Assignee.DisplayName
                });
            }
            catch
            {
                return new List<Issue>();
            }
        }
    }
}