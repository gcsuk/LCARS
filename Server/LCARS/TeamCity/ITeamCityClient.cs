using LCARS.TeamCity.Models;
using Refit;

namespace LCARS.TeamCity;

[Headers(new[] { "Accept: application/json" })]
public interface ITeamCityClient
{
    [Get("/projects")]
    Task<Project> GetProjects([Header("Authorization")] string token);

    [Get("/buildTypes/id:{buildTypeId}/builds")]
    Task<BuildComplete> GetBuilds([Header("Authorization")] string token, string buildTypeId);

    [Get("/buildTypes/id:{buildTypeId}/builds/running:true")]
    Task<BuildRunning> GetBuildRunning([Header("Authorization")] string token, string buildTypeId);
}