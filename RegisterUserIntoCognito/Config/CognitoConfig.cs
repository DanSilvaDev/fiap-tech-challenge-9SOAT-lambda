using Amazon;

namespace RegisterUserIntoCognito.Config;
internal class CognitoConfig
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public RegionEndpoint Region { get; set; }
}
