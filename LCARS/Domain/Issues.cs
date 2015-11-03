using System.Collections.Generic;
using System.Linq;
using LCARS.ViewModels.Issues;

namespace LCARS.Domain
{
    public class Issues : IIssues
    {
        private readonly Repository.IIssues _repository;
        private readonly Repository.IRepository<Models.Issues.Query> _settingsRepository;

        public Issues(Repository.IIssues repository, Repository.IRepository<Models.Issues.Query> settingsRepository)
        {
            _repository = repository;
            _settingsRepository = settingsRepository;
        }

        public IEnumerable<Query> GetQueries(string path)
        {
            return _settingsRepository.GetList(path).Select(q => new Query
            {
                Id = q.Id,
                Jql = q.Jql
            });
        }

        public IEnumerable<Issue> Get(string query)
        {
            return _repository.Get(query).Issues.OrderByDescending(i => i.Fields.Created).Select(i => new Issue
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
    }
}