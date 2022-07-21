using LCARS.Models.TeamCity;

namespace LCARS.Services;

public interface ITeamCityService
{
    Task<IEnumerable<Project>> GetProjects();

    Task<IEnumerable<Build>> GetBuildsComplete();

    Task<IEnumerable<Build>> GetBuildsRunning();
}