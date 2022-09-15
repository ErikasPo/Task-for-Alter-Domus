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

        /// Calls GitHub user repositories API returns model with Repositories view 
        public ActionResult Repositories(string GitHubUsername)
        {
            RepositoriesViewModel model = new RepositoriesViewModel();
            model.RepositoriesList = new List<RepositoriesModel>();

            if (string.IsNullOrEmpty(GitHubUsername))
            {
                return View(model);
            }

            var gitHubClient = new HttpClient();
            gitHubClient.DefaultRequestHeaders.Add(USERAGENT, REQUEST);
            gitHubClient.BaseAddress = new Uri(GITHUBUSERSURL);
            HttpResponseMessage response = gitHubClient.GetAsync(GitHubUsername + REPOS).Result;

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string repositoryData = response.Content.ReadAsStringAsync().Result;
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
            }
            return View(model);
        }
    }
}

