using System.Threading.Tasks;
using Refit;
using LCARS.Models.Issues;

namespace LCARS.Services
{
    public interface IIssuesClient
    {
        [Get("/search?maxResults=1000&jql={jql}")]
        Task<Parent> GetIssues([Header("Authorization")] string token, string jql);
    }
}