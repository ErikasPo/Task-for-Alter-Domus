using Newtonsoft.Json;

namespace GitHubApp.Models
{   // Followers model data for API
    public class FollowersModel
    {
        [JsonProperty("login")]
        public static string Login { get; set; }
    }
}
