using LCARS.BitBucket.Models;
using Refit;

namespace LCARS.BitBucket;

public interface IBitBucketAuthClient
{
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    [Post("/access_token")]
    Task<ApiAccessToken> GetAccessToken([Header("Authorization")] string token, [Body(BodySerializationMethod.UrlEncoded)] FormUrlEncodedContent content);
}