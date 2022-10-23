namespace LCARS.Data;

public record Build
{
    public int? Id { get; set; } = default;

    public string? Number { get; set; } = default;

    public string? Branch { get; set; } = default;

    public string? Status { get; set; } = default;

    public string? State { get; set; } = default;

    public int? PercentageComplete { get; set; } = default;
}