using Newtonsoft.Json;

namespace GitHubApp.Models
{    //Repositories model data for API
    public class RepositoriesModel
    {
        [JsonProperty("name")]
        public static string Name { get; set; }
    }
}
