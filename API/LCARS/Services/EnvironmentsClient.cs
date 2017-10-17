using System.Threading.Tasks;
using LCARS.Models.Environments;
using Refit;

namespace LCARS.Services
{
    public interface IEnvironmentsClient
    {
        [Get("/api/ping")]
        Task<SiteVersion> GetVersion();
    }
}