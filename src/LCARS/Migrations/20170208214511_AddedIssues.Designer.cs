using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LCARS.Repositories;

namespace LCARS.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170208214511_AddedIssues")]
    partial class AddedIssues
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LCARS.Models.Environments.Environment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("SiteId");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("Environments");
                });

            modelBuilder.Entity("LCARS.Models.Environments.Site", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("LCARS.Models.GitHub.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BaseUrl");

                    b.Property<int>("BranchThreshold");

                    b.Property<string>("Owner");

                    b.Property<int>("PullRequestThreshold");

                    b.Property<string>("RepositoriesString");

                    b.HasKey("Id");

                    b.ToTable("GitHubSettings");
                });

            modelBuilder.Entity("LCARS.Models.Issues.Query", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Deadline");

                    b.Property<string>("Jql");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("IssueQueries");
                });

            modelBuilder.Entity("LCARS.Models.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BuildServerPassword");

                    b.Property<string>("BuildServerUrl");

                    b.Property<string>("BuildServerUsername");

                    b.Property<string>("DeploymentsServerKey");

                    b.Property<string>("DeploymentsServerUrl");

                    b.Property<string>("GitHubPassword");

                    b.Property<string>("GitHubUsername");

                    b.Property<string>("IssuesPassword");

                    b.Property<string>("IssuesUrl");

                    b.Property<string>("IssuesUsername");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("LCARS.Models.Environments.Environment", b =>
                {
                    b.HasOne("LCARS.Models.Environments.Site", "Site")
                        .WithMany("Environments")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
