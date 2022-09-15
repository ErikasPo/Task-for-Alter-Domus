using GitHubApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

namespace GitHubApp.Controllers
{
    ///This controller is responsible for Followers view
    public class FollowersController : Controller
    {
        private const string GITHUBUSERSURL = "https://api.github.com/users/";
        private const string FOLLOWERS = "/followers";
        private const string USERAGENT = "User-Agent";
        private const string REQUEST = "request";

        /// Calls GitHub user followers API returns model with Followers view 
        public ActionResult Followers(string GitHubUsername)
        {
            FollowersViewModel model = new FollowersViewModel();
            model.FollowersList = new List<FollowersModel>();

            if (string.IsNullOrEmpty(GitHubUsername))
            {
                return View(model);
            }

            var gitHubClient = new HttpClient();
            gitHubClient.DefaultRequestHeaders.Add(USERAGENT, REQUEST);
            gitHubClient.BaseAddress = new Uri(GITHUBUSERSURL);
            HttpResponseMessage response = gitHubClient.GetAsync(GitHubUsername + FOLLOWERS).Result;

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string followerData = response.Content.ReadAsStringAsync().Result;
                    List<FollowersModel> followers = JsonConvert.DeserializeObject<List<FollowersModel>>(followerData);

                    model.StatusCode = (int)response.StatusCode;

                    if (followers != null)
                    {
                        model.FollowersList = followers;
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Error occured when getting followers from GitHub API." + ex.StackTrace);
                    return View(new List<FollowersModel>());
                }
            }
            return View(model);
        }
    }
}
