namespace LCARS.TeamCity.Responses;

public record Build
{
    public string? BuildTypeId { get; set; } = default;

    public string? BuildNumber { get; set; } = default;

    public string? Branch { get; set; } = default;

    public string? Status { get; set; } = default;

    public string? State { get; set; } = default;

    public int PercentageComplete { get; set; } = 100;

    public int ElapsedSeconds { get; set; }

    public int EstimatedTotalSeconds { get; set; }

    public string? CurrentStageText { get; set; }

    public bool ProbablyHanging { get; set; }

    public string? WebUrl { get; set; }
}