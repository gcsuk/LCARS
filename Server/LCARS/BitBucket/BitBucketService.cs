using LCARS.BitBucket.Models;
using LCARS.BitBucket.Responses;

namespace LCARS.BitBucket;

public class BitBucketService : IBitBucketService
{
    private readonly IConfiguration _configuration;
    private readonly IBitBucketAuthClient _bitBucketAuthClient;
    private readonly IBitBucketClient _bitBucketClient;
    private readonly string _owner;
    private readonly IEnumerable<string> _repositories;

    public BitBucketService(IConfiguration configuration, IBitBucketAuthClient bitBucketAuthClient, IBitBucketClient bitBucketClient)
    {
        _configuration = configuration;
        _bitBucketAuthClient = bitBucketAuthClient;
        _bitBucketClient = bitBucketClient;
        _owner = configuration["BitBucket:Owner"] ?? "";
        _repositories = configuration.GetSection("BitBucket:Repositories").Get<List<string>>() ?? Enumerable.Empty<string>();
    }

    public async Task<IEnumerable<Branch>> GetBranches()
    {
        var accessToken = await GetAccessToken();

        var branches = new List<Branch>();

        foreach (var repository in _repositories)
        {
            var page = 1;

            while (true)
            {
                var branchSet = await _bitBucketClient.GetBranches(accessToken, _owner, repository, 100, page);

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

    public async Task<IEnumerable<PullRequest>> GetPullRequests()
    {
        var accessToken = await GetAccessToken();

        var pullRequests = new List<PullRequest>();

        foreach (var repository in _repositories)
        {
            var page = 1;

            while (true)
            {
                var pulls = await _bitBucketClient.GetPullRequests(accessToken, _owner, repository, 50, page);

                if (!pulls.Values.Any())
                    break;

                pullRequests.AddRange(pulls.Values.Select(p => new PullRequest
                {
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

    private async Task<string> GetAccessToken()
    {
        var authenticationString = $"{_configuration["BitBucket:Username"]}:{_configuration["BitBucket:Password"]}";
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