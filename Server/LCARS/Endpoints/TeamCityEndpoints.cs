using LCARS.Endpoints.Internal;
using LCARS.Models.GitHub;
using LCARS.Services;

namespace LCARS.Endpoints;

public class TeamCityEndpoints : IEndpoints
{
    private const string Tag = "TeamCity";
    private const string BaseRoute = "teamcity";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}/projects", GetProjects)
            .WithName("GetProjects")
            .Produces<IEnumerable<Project>>(200)
            .WithTags(Tag);
    }

    internal static async Task<IResult> GetProjects(ITeamCityService teamCityService) => Results.Ok(await teamCityService.GetProjects());

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITeamCityService, TeamCityService>();
    }
}