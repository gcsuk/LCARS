namespace LCARS.TeamCity.Responses;

public record Build
{
    public string? DisplayName { get; set; }

    public string? BuildTypeId { get; set; }

    public string? BuildNumber { get; set; }

    public string? Branch { get; set; }

    public string? Status { get; set; }

    public string? State { get; set; }

    public int PercentageComplete { get; set; } = 100;

    public int ElapsedSeconds { get; set; }

    public int EstimatedTotalSeconds { get; set; }

    public string? CurrentStageText { get; set; }

    public bool ProbablyHanging { get; set; }

    public string? WebUrl { get; set; }
}