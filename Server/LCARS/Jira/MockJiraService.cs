using LCARS.Configuration.Models;
using LCARS.Jira.Responses;

namespace LCARS.Jira
{
    public class MockJiraService : IJiraService
    {
        public async Task<IEnumerable<Issue>> GetIssues(JiraSettings settings) => await Task.FromResult(new List<Issue> {
            new Issue
            {
                Name = "A User Story",
                IssueType = "Story",
                Description = "A user story of some kind",
                Status = "In Progress"
            },
            new Issue
            {
                Name = "A Task",
                IssueType = "Task",
                Description = "A task of some kind",
                Status = "Ready"
            }
        });
    }
}