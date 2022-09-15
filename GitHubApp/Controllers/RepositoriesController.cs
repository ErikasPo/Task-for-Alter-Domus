using GitHubApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;

namespace GitHubApp.Controllers
{
    ///This controller is responsible for Repositories view
    public class RepositoriesController : Controller
    {
        private const string GITHUBUSERSURL = "https://api.github.com/users/";
        private const string REPOS = "/repos";
        private const string USERAGENT = "User-Agent";
        private const string REQUEST = "request";
        private HttpClient gitHubClient;

        /// Creates new HttpClient and sets API parameters
        public RepositoriesController()
        {
            gitHubClient = new HttpClient();
            gitHubClient.DefaultRequestHeaders.Add(USERAGENT, REQUEST);
            gitHubClient.BaseAddress = new Uri(GITHUBUSERSURL);
        }

        /// Calls GitHub user repositories API returns model with Repositories view 
        public ActionResult Repositories(string GitHubUsername)
        {
            RepositoriesViewModel model = new RepositoriesViewModel();
            model.RepositoriesList = new List<RepositoriesModel>();
            model.StatusCode = 0;
            if (string.IsNullOrEmpty(GitHubUsername))
            {
                return View(model);
            }

            HttpResponseMessage response = gitHubClient.GetAsync(GitHubUsername + REPOS).Result;

            string repositoryData = "";
            if (response.IsSuccessStatusCode)
            {
                repositoryData = response.Content.ReadAsStringAsync().Result;
            }
            else   /// When API rate limit is exceeded for testing
            //    repositoryData = "{ \"login\": \"erikaspo\" }";
            //model.StatusCode = 200;
            {
                model.StatusCode = (int)response.StatusCode;
                return View(model);
            }


            try
            {
                List<RepositoriesModel> repositories = JsonConvert.DeserializeObject<List<RepositoriesModel>>(repositoryData);
                model.StatusCode = (int)response.StatusCode;
                if (repositories != null)
                {
                    model.RepositoriesList = repositories;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error occured when getting repositories from GitHub API." + ex.StackTrace);
                return View(new List<RepositoriesModel>());
            }

            return View(model);
        }
    }
}

