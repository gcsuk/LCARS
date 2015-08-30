using System;
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

	    public Boards SelectBoard()
	    {
	       return (Boards)new Random(Guid.NewGuid().GetHashCode()).Next(1, Enum.GetNames(typeof(Boards)).Length + 1);
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

		public RedAlert GetRedAlertSettings(string fileName)
		{
			return _repository.GetRedAlertSettings(fileName);
		}

		public void UpdateRedAlertSettings(string fileName, bool isEnabled, string targetDate)
		{
			_repository.UpdateRedAlertSettings(fileName, isEnabled, targetDate);
		}

        public IEnumerable<Build> GetBuilds(string path)
        {
            return _repository.GetBuilds(path);
        }

        public IEnumerable<Build> GetBuildStatus(IEnumerable<int> buildTypeIds)
        {
            var builds = new List<Build>();

            // Get all running builds
            var buildsRunning = _repository.GetBuildsRunning();

            foreach (var buildTypeId in buildTypeIds)
            {
                var build = new Build {TypeId = buildTypeId};

                if (buildsRunning.ContainsKey(buildTypeId))
                {
                    build.Progress = _repository.GetBuildProgress(buildsRunning.Single(b => b.Key == buildTypeId).Value);
                }
                else
                {
                    var lastBuildStatus = _repository.GetLastBuildStatus(buildTypeId);

                    build.Status = lastBuildStatus.Key;
                    build.Number = lastBuildStatus.Value;
                //http://teamcity.bedegaming.com/app/rest/builds?locator=buildType:(id:bt81)
                    //build.Status =
                    //    string.Format(
                    //        "http://teamcity.bedegaming.com/app/rest/builds/buildType:(id:bt{0})/statusIcon",
                    //        build.TypeId);
                }

                builds.Add(build);
            }

            return builds;
        }
    }
}