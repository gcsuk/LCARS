﻿namespace LCARS.BitBucket.Responses;

public record PullRequest
{
    public string Repository { get; set; }
    public int Number { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? State { get; set; }
    public string? CreatedOn { get; set; }
    public string? UpdatedOn { get; set; }
    public string? Author  { get; set; }
    public int CommentCount { get; set; }
}