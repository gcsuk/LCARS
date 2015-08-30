﻿using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Autofac;
using Autofac.Integration.Mvc;
using LCARS.Domain;
using LCARS.Services;

namespace LCARS
{
	public class AutofacConfig
	{
		public static void RegisterDependencies()
		{
		    var credentials = GetCredentials();

			var builder = new ContainerBuilder();

			// Register your MVC controllers.
			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			builder.RegisterType<Builds>().As<IBuilds>();
            builder.RegisterType<Common>().As<ICommon>();
            builder.RegisterType<Environments>().As<IEnvironments>();
		    builder.RegisterType<Repository>()
		        .As<IRepository>()
		        .WithParameter("username", credentials.Key)
		        .WithParameter("password", credentials.Value);

			// Set the dependency resolver to be Autofac.
			var container = builder.Build();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}

        // Cant put credentials on GitHub so segreated them into excluded XML file
	    private static KeyValuePair<string, string> GetCredentials()
	    {
             var doc = XDocument.Load(HttpContext.Current.Server.MapPath(@"~/App_Data/Creds.xml"));

	        return new KeyValuePair<string, string>(doc.Root.Element("Username").Value, doc.Root.Element("Password").Value);
	    }
	}
}