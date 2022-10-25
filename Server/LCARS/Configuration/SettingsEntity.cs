﻿using Azure;
using Azure.Data.Tables;

namespace LCARS.Configuration;

public record SettingsEntity : ITableEntity
{
    public string? PartitionKey { get; set; }
    public string? RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; } = ETag.All;

    public bool Enabled { get; set; }
    public string? BaseUrl { get; set; }
    public string? Key { get; set; }
    public string? Owner { get; set; }
    public string? Repositories { get; set; }
    public int? BranchThreshold { get; set; }
    public int? PullRequestThreshold { get; set; }

    public string? AuthUrl { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public string? AccessToken { get; set; }
    public string? BuildTypeIds { get; set; }

    public string? Projects { get; set; }
}