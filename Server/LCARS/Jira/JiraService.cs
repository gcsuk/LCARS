using LCARS.Jira.Models;

namespace LCARS.Jira
{
    public class JiraService : IJiraService
    {
        private readonly string _apiKey;
        private readonly IJiraClient _jiraClient;

        public JiraService(IConfiguration configuration, IJiraClient jiraClient)
        {
            _apiKey = $"Bearer {configuration["Jira:AccessToken"]}";
            _jiraClient = jiraClient;
        }

        public async Task<IEnumerable<Issue>> GetIssues()
        {
            var response = await _jiraClient.GetIssues(_apiKey);

            return response.Issues.Select(i => new Issue
            {
                Name = i.Fields?.Summary,
                IssueType = i?.Fields?.IssueType?.Name,
                Description = i?.Fields?.Description,
                Status = i?.Fields?.Status?.Name,
            });
        }
    }
}