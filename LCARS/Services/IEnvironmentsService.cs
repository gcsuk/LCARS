using System.Collections.Generic;
using LCARS.ViewModels.Environments;

namespace LCARS.Services
{
    public interface IEnvironmentsService
    {
        IEnumerable<Site> GetSites();
        Site AddSite(Site site);
        void UpdateSite(Site site);
        void DeleteSite(int id);
    }
}