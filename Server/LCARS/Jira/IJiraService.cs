using LCARS.Jira.Models;

namespace LCARS.Jira;

public interface IJiraService
{
    Task<IEnumerable<Issue>> GetIssues();
}