using LCARS.Configuration;
using LCARS.Endpoints;
using LCARS.Jira.Responses;
using Refit;

namespace LCARS.Jira;

public class JiraEndpoints : IEndpoints
{
    private const string Tag = "Jira";
    private const string BaseRoute = "jira";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}/issues", GetIssues)
            .WithName("GetIssues")
            .Produces<IEnumerable<Issue>>(200)
            .WithTags(Tag);
    }

    internal static async Task<IResult> GetIssues(IJiraService jiraService, ISettingsService settingsService)
    {
        var settings = await settingsService.GetJiraSettings();

        var issues = await jiraService.GetIssues(settings);

        return Results.Ok(issues);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration["TeamCity:BaseUrl"];

        if (string.IsNullOrEmpty(baseUrl))
            return;

        services.AddRefitClient<IJiraClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        if (Convert.ToBoolean(configuration["EnableMocks"]))
            services.AddScoped<IJiraService, MockJiraService>();
        else
            services.AddScoped<IJiraService, JiraService>();
    }
}