using Newtonsoft.Json;
using Octokit;
using Octokit.Internal;
using SearchEngineAPI.Managers.Favorite;
using SearchEngineAPI.Models;
using SearchEngineAPI.Models.SearchResult;

namespace SearchEngineAPI.Managers
{
    public class GitHubSearchEngineManager : IGitHubSearchEngineManager
    {
        private readonly ILogger<GitHubSearchEngineManager> _logger;
        private readonly IFavoriteManager _favoriteManager;
        public GitHubSearchEngineManager(IFavoriteManager favoriteManager, ILogger<GitHubSearchEngineManager> logger)
        {
            _favoriteManager = favoriteManager;
            _logger = logger;
        }
        public async Task<List<SearchResult>> Search(string value, Guid userId)
        {
            _logger.LogDebug($"GitHubSearchEngineManager => Search => Value : {value} UserId : {userId}");
            try
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
                    IsFavorite = favorites.Any(w => w.GitHubId == s.Id)

                }).ToList();

                return searchResults;
            }
            catch (Exception ex)
            {
                _logger.LogError($"GitHubSearchEngineManager => Search => Message : {ex.Message}");
                throw new Exception("Failed to Search");
            }
        }


    }
}
