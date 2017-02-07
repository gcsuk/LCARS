using LCARS.Models;
using LCARS.Models.Environments;
using Microsoft.EntityFrameworkCore;

namespace LCARS.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) { }

        public virtual DbSet<Settings> Settings { get; set; }

        public virtual DbSet<Environment> Environments { get; set; }

        public virtual DbSet<Site> Sites { get; set; }

        public virtual DbSet<Models.GitHub.Settings> GitHubSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateBuildsEntity(modelBuilder);

            CreateGitHubSettingsEntity(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void CreateBuildsEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Settings>()
                .Property(t => t.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Site>()
                .Property(t => t.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Environment>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Environment>()
                .Property(t => t.Name)
                .IsRequired();

            modelBuilder.Entity<Site>()
                .HasMany(d => d.Environments);

            modelBuilder.Entity<Environment>()
               .HasOne(g => g.Site)
               .WithMany(p => p.Environments)
               .HasForeignKey(d => d.SiteId);
        }

        private void CreateGitHubSettingsEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.GitHub.Settings>()
                .Property(t => t.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Models.GitHub.Settings>()
                .Ignore(t => t.Repositories);
        }
    }
}