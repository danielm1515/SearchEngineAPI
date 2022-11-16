using SearchEngineAPI.Models.SearchResult;

namespace SearchEngineAPI.Managers.Favorite
{
    public interface IFavoriteManager
    {
        public Task<List<Models.Favorite>> GetFavorites(Guid userId);
        public Task<bool> RemoveFavorite(Guid favoriteId, Guid userId);
        public Task<bool> AddFavorite(SearchResult searchResult,Guid userId);
    }
}
