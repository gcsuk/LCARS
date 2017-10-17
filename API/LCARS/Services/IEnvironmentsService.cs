using System.Collections.Generic;
using System.Threading.Tasks;
using LCARS.Models.Environments;

namespace LCARS.Services
{
    public interface IEnvironmentsService
    {
        Task<IEnumerable<Site>> GetSites();
        Task<Site> AddSite(Site site);
        Task<SiteEnvironment> AddEnvironment(SiteEnvironment environment);
        Task UpdateSite(Site environment);
        Task UpdateEnvironment(SiteEnvironment environment);
        Task DeleteSite(int id);
        Task DeleteEnvironment(int id);
    }
}