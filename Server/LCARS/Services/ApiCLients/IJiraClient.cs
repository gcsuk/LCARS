using LCARS.Models.Jira;
using Refit;

namespace LCARS.Services.ApiClients;

public interface IJiraClient
{
    [Get("/search?jql=project=lcar&fields=status,issuetype,summary,description")]
    Task<IssueResponse> GetIssues([Header("Authorization")] string token);
}