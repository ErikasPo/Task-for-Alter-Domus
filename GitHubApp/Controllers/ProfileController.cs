using GitHubApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;

namespace GitHubApp.Controllers
{
    ///This controller is responsible for Index,Repositories and Followers views
    public class ProfileController : Controller
    {
        private const string GITHUBUSERSURL = "https://api.github.com/users/";
        private const string EMPTYFIELD = "This field is empty! Please fill in the field!";
        private const string USERAGENT = "User-Agent";
        private const string REQUEST = "request";

        ///Calls GitHub user profile API returns model with Index view 
        public ActionResult Profile(string GitHubUsername)
        {
            ProfileModel model = new ProfileModel();

            if (string.IsNullOrEmpty(GitHubUsername))
            {
                return View(model);
            }

            var gitHubClient = new HttpClient();
            gitHubClient.BaseAddress = new Uri(GITHUBUSERSURL);
            gitHubClient.DefaultRequestHeaders.Add(USERAGENT, REQUEST);
            HttpResponseMessage response = gitHubClient.GetAsync(GitHubUsername).Result;

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string profileData = response.Content.ReadAsStringAsync().Result;
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
                    return View(new ProfileModel());
                }
            }
            return View(model);
        }
    }
}
