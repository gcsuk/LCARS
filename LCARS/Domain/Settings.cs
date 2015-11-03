using System;
using LCARS.Repository;
using LCARS.ViewModels;

namespace LCARS.Domain
{
    public class Settings
    {
        private readonly IRepository<Models.Settings> _repository;

        public Settings(IRepository<Models.Settings> repository)
        {
            _repository = repository;
        }

        public ViewModels.Settings GetSettings(string filePath)
        {
            var settings = _repository.Get(filePath);

            return new ViewModels.Settings
            {
                BuildServerUsername = settings.BuildServerCredentials.Username,
                BuildServerPassword = settings.BuildServerCredentials.Password,
                DeploymentServerPath = settings.DeploymentServerPath,
                DeploymentServerKey = settings.DeploymentServerKey,
                IssuesUrl = settings.IssuesUrl,
                IssuesUsername = settings.IssuesUsername,
                IssuesPassword = settings.IssuesPassword
            };
        }

        public static Boards SelectBoard()
        {
            return (Boards)new Random(Guid.NewGuid().GetHashCode()).Next(1, Enum.GetNames(typeof(Boards)).Length + 1);
        }
    }
}