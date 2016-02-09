using LCARS.Repository;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using LCARS.Models;

namespace LCARS.Domain
{
    public class ConfigInitialiser
    {
        private readonly IRepository<Models.Settings> _settingsRepository;
        private readonly IRepository<Models.Screens.Screen> _screenRepository;
        private readonly IRepository<Models.RedAlert> _redAlertRepository;
        private readonly IRepository<Models.Builds.BuildProject> _buildsRepository;
        private readonly IRepository<Models.Deployments.Environment> _deploymentsRepository;
        private readonly IRepository<Models.Environments.Tenant> _environmentsRepository;
        private readonly IRepository<Models.Issues.Query> _issuesRepository;
        private readonly string _settingsFilePath;
        private readonly string _screenFilePath;
        private readonly string _redAlertFilePath;
        private readonly string _buildsFilePath;
        private readonly string _deploymentsFilePath;
        private readonly string _environmentsFilePath;
        private readonly string _issuesFilePath;

        public ConfigInitialiser()
        {
            _settingsRepository = new SettingsRepository<Models.Settings>();
            _screenRepository = new SettingsRepository<Models.Screens.Screen>();
            _redAlertRepository = new SettingsRepository<Models.RedAlert>();
            _buildsRepository = new SettingsRepository<Models.Builds.BuildProject>();
            _deploymentsRepository = new SettingsRepository<Models.Deployments.Environment>();
            _environmentsRepository = new SettingsRepository<Models.Environments.Tenant>();
            _issuesRepository = new SettingsRepository<Models.Issues.Query>();

            _settingsFilePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["GlobalSettingsPath"]);
            _screenFilePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ScreenSettingsPath"]);
            _redAlertFilePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["RedAlertSettingsPath"]);
            _buildsFilePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BuildsSettingsPath"]);
            _deploymentsFilePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["DeploymentsSettingsPath"]);
            _environmentsFilePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["EnvironmentsSettingsPath"]);
            _issuesFilePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["IssuesSettingsPath"]);
        }

        public void Generate()
        {
            if (File.Exists(_settingsFilePath))
            {
                // No need to generate as the files already exist
                return;
            }

            var settings = new Models.Settings
            {
                BuildServerCredentials = new Credentials
                {
                    Domain = "",
                    Username = "",
                    Password = ""
                },
                DeploymentServerKey = "",
                DeploymentServerPath = "",
                IssuesUsername = "",
                IssuesPassword = "",
                IssuesUrl = ""
            };

            _settingsRepository.Update(_settingsFilePath, settings);

            var screens = new List<Models.Screens.Screen>
            {
                new Models.Screens.Screen
                {
                    Id = 1,
                    Name = "Screen 1",
                    Boards = new List<Models.Screens.Board>
                    {
                        new Models.Screens.Board
                        {
                            Id = "1",
                            CategoryId = 1,
                            Argument = ""
                        }
                    }
                }
            };

            _screenRepository.UpdateList(_screenFilePath, screens);

            var redAlert = new Models.RedAlert
            {
                AlertType = ""
            };

            _redAlertRepository.Update(_redAlertFilePath, redAlert);

            var builds = new List<Models.Builds.BuildProject>
            {
                new Models.Builds.BuildProject
                {
                    Id = 1,
                    Name = "Example Team",
                    Builds = new List<Models.Builds.Build>
                    {
                        new Models.Builds.Build
                        {
                            Name = "Build 1",
                            TypeId = "bt1"
                        }
                    }
                }
            };

            _buildsRepository.UpdateList(_buildsFilePath, builds);

            var deployments = new List<Models.Deployments.Environment>
            {
                new Models.Deployments.Environment
                {
                    Id = "Environments-1",
                    OrderId = 1,
                    Name = "Example"
                }
            };

            _deploymentsRepository.UpdateList(_deploymentsFilePath, deployments);

            var environments = new List<Models.Environments.Tenant>
            {
                new Models.Environments.Tenant
                {
                    Id = 1,
                    Name = "Example 1",
                    Environments = new List<Models.Environments.Environment>
                    {
                        new Models.Environments.Environment
                        {
                            Name = "DV01",
                            Status = "OK"
                        },
                        new Models.Environments.Environment
                        {
                            Name = "QA01",
                            Status = "ISSUES"
                        },
                        new Models.Environments.Environment
                        {
                            Name = "PR01",
                            Status = "DOWN"
                        }
                    }
                },
                new Models.Environments.Tenant
                {
                    Id = 2,
                    Name = "Example 2",
                    Environments = new List<Models.Environments.Environment>
                    {
                        new Models.Environments.Environment
                        {
                            Name = "DV01",
                            Status = "ISSUES"
                        },
                        new Models.Environments.Environment
                        {
                            Name = "QA01",
                            Status = "OK"
                        },
                        new Models.Environments.Environment
                        {
                            Name = "PR01",
                            Status = "OK"
                        }
                    }
                }
            };

            _environmentsRepository.UpdateList(_environmentsFilePath, environments);

            var issues = new List<Models.Issues.Query>
            {
                new Models.Issues.Query
                {
                    Id = 1,
                    Name = "Example",
                    Jql = ""
                }
            };

            _issuesRepository.UpdateList(_issuesFilePath, issues);
        }
    }
}