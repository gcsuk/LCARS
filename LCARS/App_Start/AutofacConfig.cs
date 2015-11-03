using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Web;
using System.Web.Mvc;

namespace LCARS
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            // Cant put credentials on GitHub so segreated them into excluded JSON file
            var settings = Domain.Common.GetSettings(HttpContext.Current.Server.MapPath(@"~/App_Data/Settings.json"));

            if (string.IsNullOrEmpty(settings.BuildServerUsername))
            {
                throw new Exception("You must specify build server credentials. See repository Readme for details.");
            }

            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<Domain.Common>().As<Domain.ICommon>();
            builder.RegisterType<Domain.Environments>().As<Domain.IEnvironments>();
            builder.RegisterType<Domain.Builds>().As<Domain.IBuilds>();
            builder.RegisterType<Domain.Deployments>().As<Domain.IDeployments>();
            builder.RegisterType<Domain.Issues>().As<Domain.IIssues>();

            builder.RegisterType<Repository.Environments>().As<Repository.IEnvironments>();
            builder.RegisterType<Repository.Common>().As<Repository.ICommon>();
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