using LCARS.TeamCity.Models;
using Refit;

namespace LCARS.TeamCity;

[Headers(new[] { "Accept: application/json" })]
public interface ITeamCityClient
{
    [Get("/projects")]
    Task<Project> GetProjects([Header("Authorization")] string token);

    [Get("/builds")]
    Task<BuildComplete> GetBuildsComplete([Header("Authorization")] string token);

    [Get("/builds?locator=running:true")]
    Task<BuildRunning> GetBuildsRunning([Header("Authorization")] string token);
}