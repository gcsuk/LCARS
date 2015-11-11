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

        public IEnumerable<Query> GetQueries(string filePath)
        {
            return _settingsRepository.GetList(filePath).Select(q => new Query
            {
                Id = q.Id,
                Name = q.Name,
                Deadline = q.Deadline,
                Jql = q.Jql
            });
        }

        public bool UpdateQuery(string filePath, Query query)
        {
            var queries = _settingsRepository.GetList(filePath).ToList();

            var selectedQuery = queries.SingleOrDefault(q => q.Id == query.Id);

            if (selectedQuery == null) // New item
            {
                queries.Add(new Models.Issues.Query
                {
                    Id = query.Id,
                    Name = query.Name,
                    Deadline = query.Deadline,
                    Jql = query.Jql
                });
            }
            else // Updated item
            {
                selectedQuery.Id = query.Id;
                selectedQuery.Name = query.Name;
                selectedQuery.Deadline = query.Deadline;
                selectedQuery.Jql = query.Jql;
            }

            _settingsRepository.UpdateList(filePath, queries);

            return selectedQuery == null;
        }

        public void DeleteQuery(string filePath, int id)
        {
            var queries = _settingsRepository.GetList(filePath).ToList();

            queries.Remove(queries.SingleOrDefault(q => q.Id == id));

            _settingsRepository.UpdateList(filePath, queries);
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