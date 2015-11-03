using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Web;
using System.Web.Mvc;
using LCARS.Models;
using LCARS.Models.Environments;
using LCARS.Models.Issues;

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

            if (string.IsNullOrEmpty(settings.BuildServerUsername))
            {
                throw new Exception("You must specify build server credentials. See repository Readme for details.");
            }

            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<Domain.RedAlert>().As<Domain.IRedAlert>();
            builder.RegisterType<Domain.Environments>().As<Domain.IEnvironments>();
            builder.RegisterType<Domain.Builds>().As<Domain.IBuilds>();
            builder.RegisterType<Domain.Deployments>().As<Domain.IDeployments>();
            builder.RegisterType<Domain.Issues>().As<Domain.IIssues>();

            builder.RegisterType<Repository.SettingsRepository<RedAlert>>().As<Repository.IRepository<RedAlert>>();
            builder.RegisterType<Repository.SettingsRepository<Tenant>>().As<Repository.IRepository<Tenant>>();
            builder.RegisterType<Repository.SettingsRepository<Build>>().As<Repository.IRepository<Build>>();
            builder.RegisterType<Repository.SettingsRepository<Query>>().As<Repository.IRepository<Query>>();
            builder.RegisterType<Repository.SettingsRepository<Models.Deployments.Environment>>().As<Repository.IRepository<Models.Deployments.Environment>>();
            builder.RegisterType<Repository.Builds>()
                .As<Repository.IBuilds>()
                .WithParameter("username", settings.BuildServerUsername)
                .WithParameter("password", settings.BuildServerPassword);
            builder.RegisterType<Repository.Deployments>()
                .As<Repository.IDeployments>()
                .WithParameter("deploymentServer", settings.DeploymentServerPath)
                .WithParameter("apiKey", settings.DeploymentServerKey);
            builder.RegisterType<Repository.Issues>()
                .As<Repository.IIssues>()
                .WithParameter("url", settings.IssuesUrl)
                .WithParameter("username", settings.IssuesUsername)
                .WithParameter("password", settings.IssuesPassword);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}