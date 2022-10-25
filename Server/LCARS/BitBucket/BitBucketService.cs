using LCARS.BitBucket.Responses;
using LCARS.Configuration.Models;

namespace LCARS.BitBucket;

public class BitBucketService : IBitBucketService
{
    private readonly IBitBucketAuthClient _bitBucketAuthClient;
    private readonly IBitBucketClient _bitBucketClient;

    public BitBucketService(IBitBucketAuthClient bitBucketAuthClient, IBitBucketClient bitBucketClient)
    {
        _bitBucketAuthClient = bitBucketAuthClient;
        _bitBucketClient = bitBucketClient;
    }

    public async Task<IEnumerable<Branch>> GetBranches(BitBucketSettings settings)
    {
        var accessToken = await GetAccessToken(settings);

        var branches = new List<Branch>();

        foreach (var repository in settings.Repositories)
        {
            var page = 1;

            while (true)
            {
                var branchSet = await _bitBucketClient.GetBranches(accessToken, settings.Owner, repository, 100, page);

                if (!branchSet.Values.Any())
                    break;

                branches.AddRange(branchSet.Values.Select(b => new Branch
                {
                    Repository = repository,
                    BranchName = b.Name,
                    DateCreated = b.Target.Date,
                    User = b.Target?.Author?.User?.Name
                }));

                page++;
            }
        }

        return branches;
    }

    public async Task<IEnumerable<PullRequest>> GetPullRequests(BitBucketSettings settings)
    {
        var accessToken = await GetAccessToken(settings);

        var pullRequests = new List<PullRequest>();

        foreach (var repository in settings.Repositories)
        {
            var page = 1;

            while (true)
            {
                var pulls = await _bitBucketClient.GetPullRequests(accessToken, settings.Owner, repository, 50, page);

                if (!pulls.Values.Any())
                    break;

                pullRequests.AddRange(pulls.Values.Select(p => new PullRequest
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