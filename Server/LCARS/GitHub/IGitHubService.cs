﻿using LCARS.GitHub.Responses;

namespace LCARS.GitHub;

public interface IGitHubService
{
    Task<IEnumerable<Branch>> GetBranches();
    Task<IEnumerable<PullRequest>> GetPullRequests(bool includeComments = false);
}