using LCARS.Endpoints.Internal;

namespace LCARS.Endpoints;

public class PingEndpoints : IEndpoints
{
    private const string Tag = "Ping";
    private const string BaseRoute = "ping";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}", () => Results.NoContent())
            .WithName("Ping")
            .Produces(204)
            .WithTags(Tag);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }
}