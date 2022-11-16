using SearchEngineAPI.Models.SearchResult;
using SearchEngineAPI.Repositories.Favorite;

namespace SearchEngineAPI.Managers.Favorite
{
    public class FavoriteManager : IFavoriteManager
    {
        private readonly IFavoriteRepository _favoriteRepository;
        public FavoriteManager(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }
         
        public async Task<bool> AddFavorite(SearchResult searchResult, Guid userId)
        {
            bool result = false;
            bool isExist = await _favoriteRepository.IsFavoriteExist(searchResult, userId);
            if(!isExist)
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

        public async Task<List<Models.Favorite>> GetFavorites(Guid userId)
        {
            var results = await _favoriteRepository.GetFavorites(userId);
            return results;
        }

        public async Task<bool> RemoveFavorite(Guid favoriteId, Guid userId)
        {
            bool result = await _favoriteRepository.RemoveFavorite(favoriteId, userId);
            return result;
        }
    }
}
