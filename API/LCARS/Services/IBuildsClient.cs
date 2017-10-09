using System.Threading.Tasks;
using LCARS.Models.Builds;
using Refit;

namespace LCARS.Services
{
    [Headers("Accept: application/json")]
    public interface IBuildsClient
    {
        [Get("/guestAuth/app/rest/buildTypes/id:{buildTypeId}")]
        Task<BuildType> GetBuildTypes(string buildTypeId);
        [Get("/guestAuth/app/rest/builds/{?id}")]
        Task<BuildProject> GetBuilds();
        [Get("/guestAuth/app/rest/builds?locator=buildType:(id:{buildTypeId}),count:{count}")]
        Task<BuildProject> GetBuildsByType(string buildTypeId, int count);
        [Get("/guestAuth/app/rest/builds/id:{id}")]
        Task<Build> GetBuildDetails(int id);
        [Get("/guestAuth/app/rest/builds?locator=count:1")]
        Task<BuildProject> GetLatestBuild(string id);
        [Get("/guestAuth/app/rest/builds?locator=running:true,buildType:{buildTypeId}")]
        Task<BuildProject> GetBuildsRunning(string buildTypeId);
    }
}