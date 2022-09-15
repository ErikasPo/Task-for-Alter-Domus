using Newtonsoft.Json;

namespace GitHubApp.Models
{   // Profile model data for API
    public class ProfileModel
    {
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
        public string GitHubUsername { get; set; }
        public int StatusCode { get; set; }

    }
}