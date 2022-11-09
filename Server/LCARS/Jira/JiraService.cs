using LCARS.Configuration.Models;
using LCARS.Jira.Responses;

namespace LCARS.Jira
{
    public class JiraService : IJiraService
    {
        private readonly IJiraClient _jiraClient;

        public JiraService(IJiraClient jiraClient)
        {
            _jiraClient = jiraClient;
        }

        public async Task<IEnumerable<Issue>> GetIssues(JiraSettings settings)
        {
            List<Issue> issues = new();

            foreach (var project in settings.Projects)
            {
                var response = await _jiraClient.GetIssues(settings.AccessToken, project);

                issues.AddRange(response.Issues.Select(i => new Issue
                {
                    Name = i.Fields?.Summary,
                    IssueType = i?.Fields?.IssueType?.Name,
                    Description = i?.Fields?.Description,
                    Status = i?.Fields?.Status?.Name,
                }));
            }

            return issues;
        }
    }
}