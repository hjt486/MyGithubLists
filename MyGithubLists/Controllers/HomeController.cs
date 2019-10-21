using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Octokit;

namespace MyGithubLists.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            var client = new GitHubClient(new ProductHeaderValue("MyGithubLists"));
            var basicAuth = new Credentials("Your Github Username", "Your Github Password"); // NOTE: Replace with your GitHub username and password
            var user = await client.User.Current();

            SearchRepositoryResult results = await client.Search.SearchRepo(
                new SearchRepositoriesRequest("stars%3A>0")
                {
                    SortField = RepoSearchSort.Stars
                });

            string[,] results_array = new string[26, 6];
            for (int i = 0; i < 26; i++)
            {
                results_array[i, 0] = results.Items[i].FullName;
                results_array[i, 1] = results.Items[i].StargazersCount.ToString();
                results_array[i, 2] = results.Items[i].HtmlUrl;
                results_array[i, 3] = results.Items[i].Owner.Login;
                results_array[i, 4] = results.Items[i].CreatedAt.ToString();
                results_array[i, 5] = results.Items[i].Description;
            }

            return View(results_array);
        }
    }
}