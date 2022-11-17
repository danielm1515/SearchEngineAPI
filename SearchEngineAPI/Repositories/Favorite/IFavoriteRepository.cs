using SearchEngineAPI.Models.SearchResult;

namespace SearchEngineAPI.Repositories.Favorite
{
    public interface IFavoriteRepository
    {
        public Task<bool> IsFavoriteExist(SearchResult searchResult, Guid userId);
        public Task<List<Models.Favorite>> GetFavorites(Guid userId);
        public Task<bool> RemoveFavorite(Guid favoriteId, Guid userId);
        public Task<bool> RemoveFavorite(long gitHubId, Guid userId);
        public Task<bool> AddFavorite(Models.Favorite favorite);
    }
}
