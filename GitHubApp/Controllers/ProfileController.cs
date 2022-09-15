using GitHubApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;

namespace GitHubApp.Controllers
{
    ///This controller is responsible for Profile view
    public class ProfileController : Controller
    {
        private const string GITHUBUSERSURL = "https://api.github.com/users/";
        private const string EMPTYFIELD = "This field is empty! Please fill in the field!";
        private const string USERAGENT = "User-Agent";
        private const string REQUEST = "request";
        private HttpClient gitHubClient;

        /// Creates new HttpClient and sets API parameters
        public ProfileController()
        {
            gitHubClient = new HttpClient();
            gitHubClient.BaseAddress = new Uri(GITHUBUSERSURL);
            gitHubClient.DefaultRequestHeaders.Add(USERAGENT, REQUEST);
        }

        ///Calls GitHub user profile API returns model with Index view 
        public ActionResult Profile(string GitHubUsername)
        {
            ProfileModel model = new ProfileModel();
            model.StatusCode = 0;
            if (string.IsNullOrEmpty(GitHubUsername))
            {
                return View(model);
            }

            HttpResponseMessage response = gitHubClient.GetAsync(GitHubUsername).Result;

            string profileData = "";
            if (response.IsSuccessStatusCode)
            {
                profileData = response.Content.ReadAsStringAsync().Result;
            }
            else
            {   /// When API rate limit is exceeded for testing
                //profileData = "{ \"avatar_url\": \"\", \"login\": \"erikaspo\", \"bio\": \"\", \"company\": \"\", \"location\": \"\" }";
                //model.StatusCode = 200;
                model.StatusCode = (int)response.StatusCode;
                return View(model);
            }

            try
            {
                model = JsonConvert.DeserializeObject<ProfileModel>(profileData);
                model.StatusCode = (int)response.StatusCode;
                foreach (var property in model.GetType().GetProperties())
                {
                    if (property.GetValue(model, null) == null)
                    {
                        property.SetValue(model, EMPTYFIELD);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error occured when getting profile from GitHub API." + ex.StackTrace);
                return View(model);
            }

            return View(model);
        }
    }
}
