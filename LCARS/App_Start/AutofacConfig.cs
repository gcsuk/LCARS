using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using LCARS.Services;

namespace LCARS
{
	public class AutofacConfig
	{
		public static void RegisterDependencies()
		{
			var builder = new ContainerBuilder();

			// Register your MVC controllers.
			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			builder.RegisterType<Domain>().As<IDomain>();
			builder.RegisterType<Repository>().As<IRepository>();

			// Set the dependency resolver to be Autofac.
			var container = builder.Build();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}