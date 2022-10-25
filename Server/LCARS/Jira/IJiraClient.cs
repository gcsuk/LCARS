using LCARS.Jira.Models;
using Refit;

namespace LCARS.Jira;

public interface IJiraClient
{
    [Get("/search?jql=project={project}&fields=status,issuetype,summary,description")]
    Task<Issue> GetIssues([Header("Authorization")] string token, string project);
}