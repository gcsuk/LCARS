using System.Collections.Generic;
using System.Linq;
using LCARS.ViewModels.Environments;

namespace LCARS.Domain
{
    public class Environments : IEnvironments
    {
        private readonly Repository.IRepository<Models.Environments.Tenant> _repository;

        public Environments(Repository.IRepository<Models.Environments.Tenant> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Tenant> Get(string path)
        {
            return _repository.GetList(path).Select(t => new Tenant
            {
                Id = t.Id,
                Name = t.Name,
                Environments = t.Environments.Select(e => new Environment
                {
                    Name = e.Name,
                    Status = e.Status
                })
            });
        }

        public void Update(string path, string tenant, string environment, string currentStatus)
        {
            var tenants = _repository.GetList(path).ToList();

            var thisEnvironment = tenants.Single(t => t.Name == tenant).Environments.Single(e => e.Name == environment);

            switch (currentStatus)
            {
                case "OK":
                    thisEnvironment.Status = "ISSUES";
                    break;
                case "ISSUES":
                    thisEnvironment.Status = "DOWN";
                    break;
                case "DOWN":
                    thisEnvironment.Status = "OK";
                    break;
                default:
                    thisEnvironment.Status = "";
                    break;
            }

            _repository.UpdateList(path, tenants);
        }
    }
}