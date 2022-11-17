using SearchEngineAPI.Controllers;
using SearchEngineAPI.Models.SearchResult;
using SearchEngineAPI.Repositories.Favorite;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Octokit.Internal;
using SearchEngineAPI.Models;

namespace SearchEngineAPI.Managers.Favorite
{
    public class FavoriteManager : IFavoriteManager
    {
        private readonly ILogger<FavoriteManager> _logger;
        private readonly IFavoriteRepository _favoriteRepository;
        public FavoriteManager(IFavoriteRepository favoriteRepository, ILogger<FavoriteManager> logger)
        {
            _favoriteRepository = favoriteRepository;
            _logger = logger;
        }

        public async Task<bool> AddFavorite(SearchResult searchResult, Guid userId)
        {
            _logger.LogDebug($"FavoriteManager => AddFavorite => {JsonConvert.SerializeObject(searchResult)} UserId : {userId}");
            try
            {
                bool result = false;
                bool isExist = await _favoriteRepository.IsFavoriteExist(searchResult, userId);
                if (!isExist)
                {
                    Models.Favorite favorite = new Models.Favorite()
                    {
                        CreatedAt = searchResult.CreatedAt,
                        Description = searchResult.Description,
                        GitHubId = searchResult.GitHubId,
                        Name = searchResult.Name,
                        Url = searchResult.Url,
                        UserId = userId
                    };
                    result = await _favoriteRepository.AddFavorite(favorite);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"FavoriteManager => AddFavorite => Message : {ex.Message}");
                throw new Exception("Failed to AddFavorite");
            }
        }

        public async Task<List<Models.Favorite>> GetFavorites(Guid userId)
        {
            _logger.LogDebug($"FavoriteManager => GetFavorites => UserId : {userId}");
            try
            {
                var results = await _favoriteRepository.GetFavorites(userId);
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"FavoriteManager => GetFavorites => Message : {ex.Message}");
                throw new Exception("Failed to GetFavorites");
            }

        }

        public async Task<bool> RemoveFavorite(Guid favoriteId, Guid userId)
        {
            _logger.LogDebug($"FavoriteManager => RemoveFavorite => FavoriteId : {favoriteId} UserId : {userId}");
            try
            {
                bool result = await _favoriteRepository.RemoveFavorite(favoriteId, userId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"FavoriteManager => RemoveFavorite => Message : {ex.Message}");
                throw new Exception("Failed to RemoveFavorite");
            }
        }

        public async Task<bool> RemoveFavorite(long gitHubId, Guid userId)
        {
            _logger.LogDebug($"FavoriteManager => RemoveFavorite => GitHubId : {gitHubId} UserId : {userId}");
            try
            {
                bool result = await _favoriteRepository.RemoveFavorite(gitHubId, userId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"FavoriteManager => RemoveFavorite => Message : {ex.Message}");
                throw new Exception("Failed to RemoveFavorite");
            }
        }
    }
}
