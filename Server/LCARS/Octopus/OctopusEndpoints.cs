using LCARS.Configuration;
using LCARS.Endpoints;
using LCARS.Octopus.Responses;
using Refit;

namespace LCARS.Octopus;

public class OctopusEndpoints : IEndpoints
{
    private const string Tag = "Octopus";
    private const string BaseRoute = "octopus";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}", GetDeployments)
            .WithName("GetDeployments")
            .Produces<IEnumerable<ProjectDeployments>>(200)
            .Produces(200)
            .WithTags(Tag);
    }

    internal static async Task<IResult> GetDeployments(IOctopusService octopusService, ISettingsService settingsService)
    {
        var settings = await settingsService.GetOctopusSettings();

        var deployments= await octopusService.GetDeployments(settings);

        return Results.Ok(deployments);
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