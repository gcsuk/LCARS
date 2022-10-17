using LCARS.Jira.Models;
using Refit;

namespace LCARS.Jira;

public interface IJiraClient
{
    [Get("/search?jql=project=lcar&fields=status,issuetype,summary,description")]
    Task<IssueResponse> GetIssues([Header("Authorization")] string token);
}