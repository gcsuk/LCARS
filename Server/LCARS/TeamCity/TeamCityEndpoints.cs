﻿using LCARS.Configuration;
using LCARS.Endpoints;
using LCARS.TeamCity.Responses;
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

        app.MapGet($"{BaseRoute}/builds", GetBuilds)
            .WithName("GetBuilds")
            .Produces<IEnumerable<Build>>(200)
            .WithTags(Tag);
    }

    internal static async Task<IEnumerable<Project>> GetProjects(ITeamCityService teamCityService, ISettingsService settingsService)
    {
        var settings = await settingsService.GetTeamCitySettings();

        return await teamCityService.GetProjects(settings);
    }

    internal static async Task<IEnumerable<Build>> GetBuilds(ITeamCityService teamCityService, ISettingsService settingsService)
    {
        var settings = await settingsService.GetTeamCitySettings();

        return await teamCityService.GetBuilds(settings);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration["TeamCity:BaseUrl"];

        if (string.IsNullOrEmpty(baseUrl))
            return;

        services.AddRefitClient<ITeamCityClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        if (Convert.ToBoolean(configuration["EnableMocks"]))
            services.AddScoped<ITeamCityService, MockTeamCityService>();
        else
            services.AddScoped<ITeamCityService, TeamCityService>();
    }
}