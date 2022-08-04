using LCARS.Models.Jira;

namespace LCARS.Services;

public interface IJiraService
{
    Task<IEnumerable<Issue>> GetIssues();
}