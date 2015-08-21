using System.Collections.Generic;
using System.Linq;
using LCARS.Models;

namespace LCARS.Services
{
	public class Domain : IDomain
	{
		private readonly IRepository _repository;

		public Domain(IRepository repository)
		{
			_repository = repository;
		}

		public IEnumerable<Tenant> GetStatus(string path)
		{
			return _repository.GetStatus(path).ToList();
		}

		public void UpdateStatus(string path, string tenant, string dependency, string environment, string currentStatus)
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

		public AutoDeploy GetAutoDeploySettings(string fileName)
		{
			return _repository.GetAutoDeploySettings(fileName);
		}

		public void UpdateAutoDeploySettings(string fileName, bool isEnabled, string targetDate)
		{
			_repository.UpdateAutoDeploySettings(fileName, isEnabled, targetDate);
		}

        public IEnumerable<Build> GetBuildStatus(string path)
        {
            return _repository.GetBuildStatus(path).ToList();
        }
    }
}