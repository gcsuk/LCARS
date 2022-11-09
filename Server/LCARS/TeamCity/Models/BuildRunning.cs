using System.Text.Json.Serialization;

namespace LCARS.TeamCity.Models;

public record BuildRunning
{
    public string? BuildTypeId { get; set; }

    public string? Number { get; set; }

    public string? BranchName { get; set; }

    public string? Status { get; set; }

    public string? State { get; set; }

    public string? WebUrl{ get; set; }


    [JsonPropertyName("running-info")]
    public RunningInfoModel RunningInfo { get; set; } = new RunningInfoModel();

    public record RunningInfoModel
    {
        public int PercentageComplete { get; set; }
        public int ElapsedSeconds { get; set; }
        public int EstimatedTotalSeconds { get; set; }
        public string? CurrentStageText { get; set; }
        public bool ProbablyHanging { get; set; }
    }
}