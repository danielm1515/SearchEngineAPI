using Octokit;
using SearchEngineAPI.Models.SearchResult;

namespace SearchEngineAPI.Managers
{
    public class GitHubSearchEngineManager : IGitHubSearchEngineManager
    {
        public async Task<List<SearchResult>> Search(string value)
        {
            var githubClient = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            var request = new SearchRepositoriesRequest(value);
            var result = await githubClient.Search.SearchRepo(request);
            var searchResults = result.Items.Select(s => new SearchResult()
            {
                Url = s.HtmlUrl,
                CreatedAt = s.CreatedAt,
                Description = s.Description,
                GitHubId = s.Id,
                Name = s.Name,
                OwnerName = s.Owner.Login,
                OwnerUrl = s.Owner.Url
            }).ToList();

            return searchResults;
        }
    }
}
