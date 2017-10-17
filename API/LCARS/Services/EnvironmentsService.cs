using System;
using System.Collections.Generic;
using LCARS.Repositories;
using System.Linq;
using System.Threading.Tasks;
using LCARS.Models.Environments;
using Refit;

namespace LCARS.Services
{
    public class EnvironmentsService : IEnvironmentsService
    {
        private readonly IRepository<Site> _sitesRepository;
        private readonly IRepository<SiteEnvironment> _environmentsRepository;

        public EnvironmentsService(IRepository<Site> sitesRepository, IRepository<SiteEnvironment> environmentsRepository)
        {
            _sitesRepository = sitesRepository;
            _environmentsRepository = environmentsRepository;
        }

        public async Task<IEnumerable<Site>> GetSites()
        {
            var sites = await _sitesRepository.GetAll();

            var environments = await _environmentsRepository.GetAll();

            foreach (var site in sites)
            {
                site.Environments = environments.Where(e => e.SiteId == site.Id).ToList();

                foreach (var siteEnvironment in site.Environments)
                {
                    try
                    {
                        var environmentsClient = RestService.For<IEnvironmentsClient>(siteEnvironment.SiteUrl);

                        siteEnvironment.Version = (await environmentsClient.GetVersion()).Version;
                        siteEnvironment.Status = "OK";
                    }
                    catch (Exception e)
                    {
                        siteEnvironment.Version = "";
                        siteEnvironment.Status = "DOWN";
                    }
                }
            }

            return sites;
        }

        public async Task<Site> AddSite(Site site)
        {
            site.Id = await _sitesRepository.Add(site);

            foreach (var env in site.Environments)
            {
                env.SiteId = site.Id;

                env.Id = await _environmentsRepository.Add(env);
            }

            return site;
        }

        public async Task<SiteEnvironment> AddEnvironment(SiteEnvironment environment)
        {
            environment.Id = await _environmentsRepository.Add(environment);

            return environment;
        }

        public async Task UpdateSite(Site site)
        {
            await _sitesRepository.Update(site);
        }

        public async Task UpdateEnvironment(SiteEnvironment environment)
        {
            await _environmentsRepository.Update(environment);
        }

        public async Task DeleteSite(int id)
        {
            await _sitesRepository.Delete(id);
        }

        public async Task DeleteEnvironment(int id)
        {
            await _environmentsRepository.Delete(id);
        }
    }
}