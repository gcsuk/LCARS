using System;
using LCARS.ViewModels;

namespace LCARS.Domain
{
    public class Common : ICommon
    {
        private readonly Repository.ICommon _repository;

        public Common(Repository.ICommon repository)
        {
            _repository = repository;
        }

        public Boards SelectBoard()
        {
            return (Boards)new Random(Guid.NewGuid().GetHashCode()).Next(1, Enum.GetNames(typeof(Boards)).Length + 1);
        }

        public RedAlert GetRedAlert(string filePath)
        {
            var settings = _repository.GetRedAlert(filePath);

            return new RedAlert
            {
                IsEnabled = settings.IsEnabled,
                AlertType = settings.AlertType,
                TargetDate = settings.TargetDate
            };
        }

        public void UpdateRedAlert(string filePath, RedAlert settings)
        {
            var model = new Models.RedAlert
            {
                IsEnabled = settings.IsEnabled,
                TargetDate = settings.TargetDate,
                AlertType = settings.AlertType
            };

            _repository.UpdateRedAlert(filePath, model);
        }

        public static Settings GetSettings(string filePath)
        {
            var settings = Repository.Common.GetSettings(filePath);

            return new Settings
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
    }
}