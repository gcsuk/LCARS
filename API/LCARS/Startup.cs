//using System.IO;
using LCARS.Filters;
using LCARS.Repositories;
using LCARS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.PlatformAbstractions;
//using Swashbuckle.Swagger.Model;

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

            services.AddTransient<IRepository<Models.GitHub.Settings>, GitHubRepository>(
                serviceProvider => new GitHubRepository(new SqlConnection(connectionString)));
            services.AddTransient<IRepository<Models.Environments.Site>, SitesRepository>(
                serviceProvider => new SitesRepository(new SqlConnection(connectionString)));
            services.AddTransient<IRepository<Models.Environments.SiteEnvironment>, EnvironmentsRepository>(
                serviceProvider => new EnvironmentsRepository(new SqlConnection(connectionString)));
            services.AddTransient<IRepository<Models.Issues.Settings>, IssueSettingsRepository>(
                serviceProvider => new IssueSettingsRepository(new SqlConnection(connectionString)));
            services.AddTransient<IRepository<Models.Issues.Query>, IssueQueriesRepository>(
                serviceProvider => new IssueQueriesRepository(new SqlConnection(connectionString)));
            services.AddTransient<IRepository<Models.Deployments.Settings>, DeploymentsRepository>(
                serviceProvider => new DeploymentsRepository(new SqlConnection(connectionString)));
            services.AddTransient<IRepository<Models.Builds.Settings>, BuildsRepository>(
                serviceProvider => new BuildsRepository(new SqlConnection(connectionString)));
            services.AddTransient<IRepository<Models.AlertCondition.AlertCondition>, AlertConditionRepository>(
                serviceProvider => new AlertConditionRepository(new SqlConnection(connectionString)));

            services.AddTransient<IEnvironmentsService, EnvironmentsService>();
            services.AddTransient<IBuildsService, BuildsService>();
            services.AddTransient<IDeploymentsService, DeploymentsService>();
            services.AddTransient<IGitHubService, GitHubService>();
            services.AddTransient<IIssuesService, IssuesService>();
            services.AddTransient<IAlertConditionService, AlertConditionService>();

            services.AddControllers(options =>
            {
                options.Filters.Add(new ApiExceptionFilter());
            });

            services.AddCors(options => options.AddPolicy("AllowAll",
                p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            /*var xmlPath = GetXmlCommentsPath();
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
            });*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            /*app.UseSwagger();
            app.UseSwaggerUi();*/
        }

        /*private static string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;
            return Path.Combine(app.ApplicationBasePath, "LCARS.xml");
        }*/
    }
}
