using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac.Integration.WebApi;
using LCARS.Models;
using LCARS.Models.Builds;
using LCARS.Models.Environments;
using LCARS.Models.Issues;
using LCARS.Models.Screens;

namespace LCARS
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            // Cant put credentials on GitHub so segregated them into excluded JSON file
            var settings =
                new Domain.Settings(new Repository.SettingsRepository<Settings>()).GetSettings(
                    HttpContext.Current.Server.MapPath(@"~/App_Data/Settings.json"));

            var builder = new ContainerBuilder();

            // Register API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<Domain.RedAlert>().As<Domain.IRedAlert>();
            builder.RegisterType<Domain.Screens>().As<Domain.IScreens>();
            builder.RegisterType<Domain.Environments>().As<Domain.IEnvironments>();
            builder.RegisterType<Domain.Builds>().As<Domain.IBuilds>();
            builder.RegisterType<Domain.Deployments>().As<Domain.IDeployments>();
            builder.RegisterType<Domain.Issues>().As<Domain.IIssues>();
            builder.RegisterType<Domain.Settings>().As<Domain.ISettings>();

            builder.RegisterType<Services.Builds>()
                .As<Services.IBuilds>()
                .WithParameter("domain", settings.BuildServerDomain)
                .WithParameter("username", settings.BuildServerUsername)
                .WithParameter("password", settings.BuildServerPassword);
            builder.RegisterType<Services.Deployments>()
                .As<Services.IDeployments>()
                .WithParameter("deploymentServer", settings.DeploymentServerPath)
                .WithParameter("apiKey", settings.DeploymentServerKey);
            builder.RegisterType<Services.Issues>()
                .As<Services.IIssues>()
                .WithParameter("url", settings.IssuesUrl)
                .WithParameter("username", settings.IssuesUsername)
                .WithParameter("password", settings.IssuesPassword);

            builder.RegisterType<Repository.SettingsRepository<RedAlert>>().As<Repository.IRepository<RedAlert>>();
            builder.RegisterType<Repository.SettingsRepository<Screen>>().As<Repository.IRepository<Screen>>();
            builder.RegisterType<Repository.SettingsRepository<Tenant>>().As<Repository.IRepository<Tenant>>();
            builder.RegisterType<Repository.SettingsRepository<BuildProject>>().As<Repository.IRepository<BuildProject>>();
            builder.RegisterType<Repository.SettingsRepository<Query>>().As<Repository.IRepository<Query>>();
            builder.RegisterType<Repository.SettingsRepository<Models.Deployments.Environment>>().As<Repository.IRepository<Models.Deployments.Environment>>();
            builder.RegisterType<Repository.SettingsRepository<Settings>>().As<Repository.IRepository<Settings>>();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            // Get HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Web API dependency resolver
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // MVC dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}