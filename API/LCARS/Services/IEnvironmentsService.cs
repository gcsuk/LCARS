using System.Collections.Generic;
using LCARS.ViewModels.Environments;
using System.Threading.Tasks;

namespace LCARS.Services
{
    public interface IEnvironmentsService
    {
        Task<IEnumerable<Site>> GetSites();
        Task<Site> AddSite(Site site);
        Task UpdateSite(Site site);
        Task DeleteSite(int id);
    }
}