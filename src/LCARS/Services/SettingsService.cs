using System;
using System.Linq;
using LCARS.Models;
using LCARS.Repositories;

namespace LCARS.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly DataContext _dataContext;

        public SettingsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Settings GetSettings()
        {
            var settings = _dataContext.Settings.SingleOrDefault();

            if (settings == null)
            {
                throw new InvalidOperationException("Invalid settings");
            }

            return new Settings
            {
                BuildServerUrl = settings.BuildServerUrl,
                BuildServerUsername = settings.BuildServerUsername,
                BuildServerPassword = settings.BuildServerPassword,
                DeploymentsServerUrl = settings.DeploymentsServerUrl,
                DeploymentsServerKey = settings.DeploymentsServerKey,
                IssuesUrl = settings.IssuesUrl,
                IssuesUsername = settings.IssuesUsername,
                IssuesPassword = settings.IssuesPassword,
                GitHubUsername = settings.GitHubUsername,
                GitHubPassword = settings.GitHubPassword
            };
        }
    }
}