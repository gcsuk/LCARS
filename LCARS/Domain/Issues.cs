using System.Collections.Generic;
using System.Linq;

namespace LCARS.Domain
{
    public class Issues : IIssues
    {
        private readonly Repository.IIssues _repository;

        public Issues(Repository.IIssues repository)
        {
            _repository = repository;
        }

        public IEnumerable<ViewModels.Issues.Issue> Get()
        {
            return _repository.Get().Issues.OrderByDescending(i => i.Fields.Created).Select(i => new ViewModels.Issues.Issue
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