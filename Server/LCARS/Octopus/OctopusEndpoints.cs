using LCARS.Configuration;
using LCARS.Endpoints;
using LCARS.Octopus.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Refit;

namespace LCARS.Octopus;

public class OctopusEndpoints : IEndpoints
{
    private const string Tag = "Octopus";
    private const string BaseRoute = "octopus";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}", GetDeployments).WithTags(Tag);
    }

    internal static async Task<Ok<IEnumerable<ProjectDeployments>>> GetDeployments(IOctopusService octopusService, ISettingsService settingsService)
    {
        var settings = await settingsService.GetOctopusSettings();

        return TypedResults.Ok(await octopusService.GetDeployments(settings));
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration["Octopus:BaseUrl"];

        if (string.IsNullOrEmpty(baseUrl))
            return;

        services.AddRefitClient<IOctopusClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        if (Convert.ToBoolean(configuration["EnableMocks"]))
            services.AddScoped<IOctopusService, MockOctopusService>();
        else
            services.AddScoped<IOctopusService, OctopusService>();
    }
}