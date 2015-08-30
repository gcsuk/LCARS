using System.Collections.Generic;
using System.Linq;
using LCARS.Models;
using LCARS.Services;

namespace LCARS.Domain
{
    public class Environments : IEnvironments
    {
        private readonly IRepository _repository;

        public Environments(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Tenant> Get(string path)
        {
            return _repository.GetStatus(path).ToList();
        }

        public void Update(string path, string tenant, string dependency, string environment, string currentStatus)
        {
            _repository.UpdateStatus(path, tenant, dependency, environment, SetNewStatus(currentStatus));
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