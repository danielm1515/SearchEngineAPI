using Octokit;
using SearchEngineAPI.Managers.Favorite;
using SearchEngineAPI.Models;
using SearchEngineAPI.Models.SearchResult;

namespace SearchEngineAPI.Managers
{
    public class GitHubSearchEngineManager : IGitHubSearchEngineManager
    {
        private readonly IFavoriteManager _favoriteManager;
        public GitHubSearchEngineManager(IFavoriteManager favoriteManager)
        {
            _favoriteManager = favoriteManager;
        }
        public async Task<List<SearchResult>> Search(string value, Guid userId)
        {
            var githubClient = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            var request = new SearchRepositoriesRequest(value);
            var result = await githubClient.Search.SearchRepo(request);
            var favorites = await _favoriteManager.GetFavorites(userId);
            var searchResults = result.Items.Select(s => new SearchResult()
            {
                Url = s.HtmlUrl,
                CreatedAt = s.CreatedAt,
                Description = s.Description,
                GitHubId = s.Id,
                Name = s.Name,
                OwnerName = s.Owner.Login,
                OwnerUrl = s.Owner.Url,
                IsFavorite = favorites.Any(w=>w.GitHubId == s.Id)

            }).ToList();

            return searchResults;
        }

       
    }
}
