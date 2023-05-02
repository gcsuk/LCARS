using System.Reflection;

namespace LCARS.Endpoints;

public static class EndpointExtensions
{
    public static void AddEndpoints(this IServiceCollection services,
        IConfiguration configuration)
    {
        var endpointTypes = GetEndpointTypesFromAssemblyContaining();

        foreach (var endpointType in endpointTypes)
            endpointType.GetMethod(nameof(IEndpoints.AddServices))!
                .Invoke(null, new object[] { services, configuration });
    }

    public static void UseEndpoints(this IApplicationBuilder app)
    {
        var endpointTypes = GetEndpointTypesFromAssemblyContaining();

        foreach (var endpointType in endpointTypes)
            endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))!
                .Invoke(null, new object[] { app });
    }

    private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining()
        => typeof(Program).Assembly.DefinedTypes
            .Where(x => !x.IsAbstract && !x.IsInterface &&
                        typeof(IEndpoints).IsAssignableFrom(x));
}
