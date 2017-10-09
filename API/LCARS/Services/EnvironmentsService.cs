using System.Collections.Generic;
using LCARS.Repositories;
using System.Linq;
using LCARS.ViewModels.Environments;
using SiteModel = LCARS.Models.Environments.Site;
using EnvironmentModel = LCARS.Models.Environments.Environment;
using System.Threading.Tasks;

namespace LCARS.Services
{
    public class EnvironmentsService : IEnvironmentsService
    {
        private readonly IRepository<SiteModel> _repository;

        public EnvironmentsService(IRepository<SiteModel> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Site>> GetSites()
        {
            return (await _repository.GetAll()).Select(t => new Site
            {
                Id = t.Id,
                Name = t.Name,
                Environments = t.Environments.Select(e => new Environment
                {
                    Id = e.Id,
                    Name = e.Name,
                    Status = e.Status
                })
            });
        }

        public async Task<Site> AddSite(Site site)
        {
            site.Id = await _repository.Add(ConvertToModel(site));

            return site;
        }

        public async Task UpdateSite(Site site)
        {
            await _repository.Update(ConvertToModel(site));
        }

        public async Task DeleteSite(int id)
        {
            await _repository.Delete(id);
        }

        private static SiteModel ConvertToModel(Site vm)
        {
            return new SiteModel
            {
                Id = vm.Id,
                Name = vm.Name,
                Environments = vm.Environments.Select(e => new EnvironmentModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Status = e.Status
                })
            };
        }
    }
}
