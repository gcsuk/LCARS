using System.Data.SqlClient;
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddTransient<IGitHubRepository, GitHubRepository>(
                serviceProvider => new GitHubRepository(new SqlConnection(connectionString)));
            services.AddTransient<IEnvironmentsRepository, EnvironmentsRepository>(
                serviceProvider => new EnvironmentsRepository(new SqlConnection(connectionString)));
            services.AddTransient<IIssueSettingsRepository, IssueSettingsRepository>(
                serviceProvider => new IssueSettingsRepository(new SqlConnection(connectionString)));
            services.AddTransient<IIssueQueriesRepository, IssueQueriesRepository>(
                serviceProvider => new IssueQueriesRepository(new SqlConnection(connectionString)));
            services.AddTransient<IDeploymentsRepository, DeploymentsRepository>(
                serviceProvider => new DeploymentsRepository(new SqlConnection(connectionString)));
            services.AddTransient<IBuildsRepository, BuildsRepository>(
                serviceProvider => new BuildsRepository(new SqlConnection(connectionString)));

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
                options.CustomSchemaIds(x => x.FullName);
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
