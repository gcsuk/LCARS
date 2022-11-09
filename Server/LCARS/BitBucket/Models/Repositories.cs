namespace LCARS.BitBucket.Models;

public record Repository
{
    public IEnumerable<RepositoryModel>? Values { get; set; }

    public record RepositoryModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}