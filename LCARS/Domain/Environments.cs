using System.Collections.Generic;
using System.Linq;
using LCARS.Models;

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
            return _repository.Get(path).ToList();
        }

        public void Update(string path, string tenant, string dependency, string environment, string currentStatus)
        {
            _repository.Update(path, tenant, dependency, environment, SetNewStatus(currentStatus));
        }

        private static string SetNewStatus(string currentStatus)
        {
            switch (currentStatus)
            {
                case "v1":
                    return "v2";
                case "v2":
                    return "v3";
                case "v3":
                    return "v4";
                case "v4":
                    return "v1";
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