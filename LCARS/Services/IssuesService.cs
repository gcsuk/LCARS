using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LCARS.Models.Issues;
using Newtonsoft.Json;
using System.Linq;
using LCARS.Repositories;

namespace LCARS.Services
{
    public class IssuesService : IIssuesService
    {
        private readonly IIssueQueriesRepository _queryRepository;
        private readonly IIssueSettingsRepository _settingsRepository;
        private Settings _settings;

        public IssuesService(IIssueQueriesRepository queryRepository, IIssueSettingsRepository settingsRepository)
        {
            _queryRepository = queryRepository;
            _settingsRepository = settingsRepository;
        }

        public async Task<IEnumerable<Query>> GetQueries(int? typeId = null)
        {
            await GetSettings();

            var queries = (await _queryRepository.GetAll()).Where(i => i.Id == (typeId ?? i.Id));

            return queries.OrderBy(r => Guid.NewGuid());
        }

        public async Task UpdateQuery(Query query)
        {
            await GetSettings();

            await _queryRepository.Update(query);
        }

        public async Task<IEnumerable<Issue>> GetIssues(string query)
        {
            await GetSettings();

            try
            {
                Parent data;

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMilliseconds(1000);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_settings.Username}:{_settings.Password}")));

                    var jsonData = await client.GetStringAsync(_settings.Url + query + "&maxResults=1000");

                    data = JsonConvert.DeserializeObject<Parent>(jsonData);
                }

                return data.Issues;
            }
            catch
            {
                return new List<Issue>();
            }
        }

        public async Task<Settings> GetSettings()
        {
            if (_settings != null)
                return _settings;

            var issuesSettings = (await _settingsRepository.GetAll()).SingleOrDefault();

            _settings = issuesSettings ?? throw new InvalidOperationException("Invalid Issues settings");

            return issuesSettings;
        }

        public async Task UpdateSettings(Settings settings)
        {
            await _settingsRepository.Update(settings);

            // Force refresh of settings data on next API call
            _settings = null;
        }
    }
}