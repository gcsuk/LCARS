using LCARS.Endpoints.Internal;
using LCARS.Models.Jira;
using LCARS.Services;

namespace LCARS.Endpoints;

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

    internal static async Task<IResult> GetIssues(IJiraService jiraService) => Results.Ok(await jiraService.GetIssues());

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJiraService, JiraService>();
    }
}