namespace LCARS.Data;

public record ProjectDeployment
{
    public string? ProjectName { get; set; }

    public IEnumerable<string?> Environments 
        => Deployments.Where(d => !string.IsNullOrEmpty(d.Environment))
        .Select(d => d.Environment);

    public List<DeploymentModel> Deployments { get; set; } = new();

    public record DeploymentModel
    {
        public string? Environment { get; set; }
        public string? ReleaseVersion { get; set; }
        public string? State { get; set; }
        public bool HasWarningsOrErrors { get; set; }
        public string? WebUrl { get; set; }
    }
}