using LCARS.Models.GitHub;
using Refit;

namespace LCARS.Services.ApiClients;

[Headers(new[] { "Accept: application/json" })]
public interface ITeamCityClient
{
    [Get("/projects")]
    Task<ProjectResponse> GetProjects([Header("Authorization")] string token);
}