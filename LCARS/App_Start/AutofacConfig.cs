using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using LCARS.ViewModels;

namespace LCARS
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            // Cant put credentials on GitHub so segreated them into excluded XML file
            var settings = GetSettings();

            if (string.IsNullOrWhiteSpace(settings.BuildServerUsername))
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

        private static Settings GetSettings()
        {
            var filePath = HttpContext.Current.Server.MapPath(@"~/App_Data/Settings.xml");

            if (!File.Exists(filePath))
            {
                throw new IOException("Settings file does not exist. Refer to ReadMe file for setup instructions.");
            }

            var doc = XDocument.Load(filePath);

            return new Settings
            {
                BuildServerUsername = doc.Root.Element("BuildServerCredentials").Element("Username").Value,
                BuildServerPassword = doc.Root.Element("BuildServerCredentials").Element("Password").Value,
                DeploymentServerPath = doc.Root.Element("DeploymentServerPath").Value,
                DeploymentServerKey = doc.Root.Element("DeploymentServerKey").Value,
                IssuesUrl = doc.Root.Element("IssuesUrl").Value,
                IssuesUsername = doc.Root.Element("IssuesUsername").Value,
                IssuesPassword = doc.Root.Element("IssuesPassword").Value
            };
        }
    }
}