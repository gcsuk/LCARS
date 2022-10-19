using LCARS.Jira.Responses;

namespace LCARS.Jira;

public interface IJiraService
{
    Task<IEnumerable<Issue>> GetIssues();
}