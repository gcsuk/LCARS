namespace LCARS.Octopus.Models;

public record Dashboard
{
    public IEnumerable<Project> Projects { get; set; } = Enumerable.Empty<Project>();

    public record Project
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Slug { get; set; }
    }

    public IEnumerable<Environment> Environments { get; set; } = Enumerable.Empty<Environment>();

    public record Environment
    {
        public string? Id { get; set; }

        public string? Name { get; set; }
    }

    public IEnumerable<Tenant> Tenants { get; set; } = Enumerable.Empty<Tenant>();

    public record Tenant
    {
        public string? Id { get; set; }

        public string? Name { get; set; }
    }

    public IEnumerable<Deployment> Items { get; set; } = Enumerable.Empty<Deployment>();

    public record Deployment
    {
        public string? Id { get; set; }
        public string? ProjectId { get; set; }
        public string? EnvironmentId { get; set; }
        public string? TenantId { get; set; }
        public string? ReleaseVersion { get; set; }
        public DateTime? CompletedTime { get; set; }
        public string? State { get; set; }
        public bool HasWarningsOrErrors { get; set; }
        public string? Duration { get; set; }
    }
}