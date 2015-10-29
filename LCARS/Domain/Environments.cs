using System.Collections.Generic;
using System.Linq;
using LCARS.ViewModels.Environments;

namespace LCARS.Domain
{
    public class Environments : IEnvironments
    {
        private readonly Repository.IEnvironments _repository;

        public Environments(Repository.IEnvironments repository)
        {
            _repository = repository;
        }

        public IEnumerable<Tenant> Get(string path)
        {
            return _repository.Get(path).Select(t => new Tenant
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
            _repository.Update(path, tenant, environment, SetNewStatus(currentStatus));
        }

        private static string SetNewStatus(string currentStatus)
        {
            switch (currentStatus)
            {
                case "OK":
                    return "ISSUES";
                case "ISSUES":
                    return "DOWN";
                case "DOWN":
                    return "OK";
                default:
                    return "";
            }
        }
    }
}