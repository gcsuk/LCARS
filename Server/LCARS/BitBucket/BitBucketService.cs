using LCARS.BitBucket.Responses;
using LCARS.Configuration;
using LCARS.Configuration.Models;

namespace LCARS.BitBucket;

public class BitBucketService : IBitBucketService
{
    private readonly ISettingsService _settingsService;
    private readonly IBitBucketAuthClient _bitBucketAuthClient;
    private readonly IBitBucketClient _bitBucketClient;

    public BitBucketService(ISettingsService settingsService, IBitBucketAuthClient bitBucketAuthClient, IBitBucketClient bitBucketClient)
    {
        _settingsService = settingsService;
        _bitBucketAuthClient = bitBucketAuthClient;
        _bitBucketClient = bitBucketClient;
    }

    public async Task<IEnumerable<BitBucketBranchSummary>> GetBranches()
    {
        var settings = await _settingsService.GetBitBucketSettings();

        var accessToken = await GetAccessToken(settings);

        var summary = new List<BitBucketBranchSummary>();

        foreach (var repository in settings.Repositories)
        {
            var branches = new BitBucketBranchSummary
            {
                Repository = repository
            };

            var page = 1;

            while (true)
            {
                var branchSet = await _bitBucketClient.GetBranches(accessToken, settings.Owner, repository, 100, page);

                if (!branchSet.Values.Any())
                    break;

                branches.Branches.AddRange(branchSet.Values.Select(b => new BitBucketBranchSummary.BitBucketBranchModel
                {
                    Name = b.Name,
                    DateCreated = b.Target.Date,
                    User = b.Target?.Author?.User?.Name
                }));

                page++;
            }

            summary.Add(branches);
        }

        return summary;
    }

    public async Task<IEnumerable<BitBucketPullRequest>> GetPullRequests()
    {
        var settings = await _settingsService.GetBitBucketSettings();

        var accessToken = await GetAccessToken(settings);

        var pullRequests = new List<BitBucketPullRequest>();

        foreach (var repository in settings.Repositories)
        {
            var page = 1;

            while (true)
            {
                var pulls = await _bitBucketClient.GetPullRequests(accessToken, settings.Owner, repository, 50, page);

                if (!pulls.Values.Any())
                    break;

                pullRequests.AddRange(pulls.Values.Select(p => new BitBucketPullRequest
                {
                    Repository = repository,
                    Number = p.Number,
                    Title = p.Title,
                    Description = p.Description,
                    State = p.State,
                    CreatedOn = p.CreatedOn,
                    UpdatedOn = p.UpdatedOn,
                    CommentCount = p.CommentCount,
                    Author = p.User.Name
                }));

                page++;
            }
        }

        return pullRequests;
    }

    private async Task<string> GetAccessToken(BitBucketSettings settings)
    {
        var authenticationString = $"{settings.Username}:{settings.Password}";
        var base64EncodedAuthenticationString = $"Basic {Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString))}";

        var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
        });

        var accessToken = await _bitBucketAuthClient.GetAccessToken(base64EncodedAuthenticationString, content);

        if (accessToken == null || accessToken.AccessToken == null)
            throw new InvalidOperationException("No access token returned by BitBucket");

        return $"Bearer {accessToken.AccessToken}";
    }
}