using System.Collections.Generic;
using LCARS.Repositories;
using System.Linq;
using LCARS.ViewModels.Environments;
using SiteModel = LCARS.Models.Environments.Site;
using EnvironmentModel = LCARS.Models.Environments.Environment;

namespace LCARS.Services
{
    public class EnvironmentsService : IEnvironmentsService
    {
        private readonly IEnvironmentsRepository _repository;

        public EnvironmentsService(IEnvironmentsRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Site> GetSites()
        {
            return _repository.GetAll().Select(t => new Site
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

        public Site AddSite(Site site)
        {
            site.Id = _repository.Add(ConvertToModel(site));

            return site;
        }

        public void UpdateSite(Site site)
        {
            _repository.Update(ConvertToModel(site));
        }

        public void DeleteSite(int id)
        {
            _repository.Delete(id);
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
