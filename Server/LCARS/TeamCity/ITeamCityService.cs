using LCARS.Configuration.Models;
using LCARS.TeamCity.Responses;

namespace LCARS.TeamCity;

public interface ITeamCityService
{
    Task<IEnumerable<Project>> GetProjects(TeamCitySettings settings);

    Task<IEnumerable<Build>> GetBuilds(TeamCitySettings settings);
}