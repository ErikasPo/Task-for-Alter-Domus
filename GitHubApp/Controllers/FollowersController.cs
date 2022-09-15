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
        private HttpClient gitHubClient;

        /// Creates new HttpClient and sets API parameters
        public FollowersController()
        {
            gitHubClient = new HttpClient();
            gitHubClient.DefaultRequestHeaders.Add(USERAGENT, REQUEST);
            gitHubClient.BaseAddress = new Uri(GITHUBUSERSURL);
        }

        /// Calls GitHub user followers API returns model with Followers view 
        public ActionResult Followers(string GitHubUsername)
        {
            FollowersViewModel model = new FollowersViewModel();
            model.FollowersList = new List<FollowersModel>();
            model.StatusCode = 0;
            if (string.IsNullOrEmpty(GitHubUsername))
            {
                return View(model);
            }

            HttpResponseMessage response = gitHubClient.GetAsync(GitHubUsername + FOLLOWERS).Result;

            string followerData = "";
            if (response.IsSuccessStatusCode)
            {
                followerData = response.Content.ReadAsStringAsync().Result;
            }
            else   /// When API rate limit is exceeded for testing
            //    followerData = "{ \"login\": \"erikaspo\" }";
            //model.StatusCode = 200;
            {
                model.StatusCode = (int)response.StatusCode;
                return View(model);
            }

            try
            {
                model.StatusCode = (int)response.StatusCode;
                List<FollowersModel> followers = JsonConvert.DeserializeObject<List<FollowersModel>>(followerData);

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

            return View(model);
        }
    }
}

