using Newtonsoft.Json;

namespace AspNet.OAuth.Providers.Models
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
