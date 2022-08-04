using LCARS.TeamCity.Models;
using Refit;

namespace LCARS.Services.ApiClients;

[Headers(new[] { "Accept: application/json" })]
public interface ITeamCityClient
{
    [Get("/projects")]
    Task<ProjectResponse> GetProjects([Header("Authorization")] string token);

    [Get("/builds")]
    Task<BuildCompleteResponse> GetBuildsComplete([Header("Authorization")] string token);

    [Get("/builds?locator=running:true")]
    Task<BuildRunningResponse> GetBuildsRunning([Header("Authorization")] string token);
}