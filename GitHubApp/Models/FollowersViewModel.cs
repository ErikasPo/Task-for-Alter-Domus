namespace GitHubApp.Models
{   /// Followers view model data for API
    public class FollowersViewModel
    {
        public List<FollowersModel> FollowersList { get; set; }
        public string GitHubUsername { get; set; }
        public int StatusCode { get; set; }
    }
}