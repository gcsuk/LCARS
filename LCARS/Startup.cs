using System.IO;
using LCARS.Filters;
using LCARS.Repositories;
using LCARS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.Swagger.Model;

namespace LCARS
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISettingsRepository, SettingsRepository>();
            services.AddTransient<IGitHubRepository, GitHubRepository>();
            services.AddTransient<IEnvironmentsRepository, EnvironmentsRepository>();
            services.AddTransient<IIssuesRepository, IssuesRepository>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IEnvironmentsService, EnvironmentsService>();
            services.AddTransient<IBuildsService, BuildsService>();
            services.AddTransient<IDeploymentsService, DeploymentsService>();
            services.AddTransient<IGitHubService, GitHubService>();
            services.AddTransient<IIssuesService, IssuesService>();

            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(new ApiExceptionFilter());
            });

            var xmlPath = GetXmlCommentsPath();
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "LCARS",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Rob King", Email = "rob@gcsuk.co.uk", Url = "www.gcsuk.co.uk" }
                });
                options.IncludeXmlComments(xmlPath);
                options.DescribeAllEnumsAsStrings();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi();
        }

        private string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;
            return Path.Combine(app.ApplicationBasePath, "LCARS.xml");
        }
    }
}
