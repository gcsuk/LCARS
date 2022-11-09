namespace LCARS.BitBucket.Responses;

public record Repository
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}