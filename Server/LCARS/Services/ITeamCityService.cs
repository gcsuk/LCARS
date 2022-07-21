using LCARS.Models.GitHub;

namespace LCARS.Services;

public interface ITeamCityService
{
    Task<IEnumerable<Project>> GetProjects();
}