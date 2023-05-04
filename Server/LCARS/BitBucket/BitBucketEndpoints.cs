using LCARS.BitBucket.Responses;
using LCARS.Configuration;
using LCARS.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Refit;

namespace LCARS.BitBucket;

public class BitBucketEndpoints : IEndpoints
{
    private const string Tag = "BitBucket";
    private const string BaseRoute = "bitbucket";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}/pullrequests", GetPullRequests).WithTags(Tag);
        app.MapGet($"{BaseRoute}/branches", GetBranches).WithTags(Tag);
    }

    internal static async Task<Ok<IEnumerable<BitBucketPullRequest>>> GetPullRequests(IBitBucketService bitBucketService, ISettingsService settingsService)
        => TypedResults.Ok(await bitBucketService.GetPullRequests());

    internal static async Task<Ok<IEnumerable<BitBucketBranchSummary>>> GetBranches(IBitBucketService bitBucketService, ISettingsService settingsService) 
        => TypedResults.Ok(await bitBucketService.GetBranches());

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        var authUrl = configuration["BitBucket:AuthUrl"];

        if (string.IsNullOrEmpty(authUrl))
            return;

        var baseUrl = configuration["BitBucket:BaseUrl"];

        if (string.IsNullOrEmpty(baseUrl))
            return;

        services.AddRefitClient<IBitBucketAuthClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(authUrl));
        services.AddRefitClient<IBitBucketClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        if (Convert.ToBoolean(configuration["EnableMocks"]))
            services.AddScoped<IBitBucketService, MockBitBucketService>();
        else
            services.AddScoped<IBitBucketService, BitBucketService>();
    }
}