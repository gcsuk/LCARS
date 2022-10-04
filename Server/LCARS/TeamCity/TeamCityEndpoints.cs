using LCARS.Endpoints;
using LCARS.Services.ApiClients;
using LCARS.TeamCity.Models;
using Refit;

namespace LCARS.TeamCity;

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

        app.MapGet($"{BaseRoute}/builds/complete", GetBuildsComplete)
            .WithName("GetBuildsComplete")
            .Produces<IEnumerable<Build>>(200)
            .WithTags(Tag);

        app.MapGet($"{BaseRoute}/builds/running", GetBuildsRunning)
            .WithName("GetBuildsRunning")
            .Produces<IEnumerable<Build>>(200)
            .WithTags(Tag);
    }

    internal static async Task<IResult> GetProjects(ITeamCityService teamCityService) => Results.Ok(await teamCityService.GetProjects());

    internal static async Task<IResult> GetBuildsComplete(ITeamCityService teamCityService) => Results.Ok(await teamCityService.GetBuildsComplete());

    internal static async Task<IResult> GetBuildsRunning(ITeamCityService teamCityService) => Results.Ok(await teamCityService.GetBuildsRunning());

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<ITeamCityClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["TeamCity:BaseUrl"]));

        if (Convert.ToBoolean(configuration["EnableMocks"]))
            services.AddScoped<ITeamCityService, MockTeamCityService>();
        else
            services.AddScoped<ITeamCityService, TeamCityService>();
    }
}