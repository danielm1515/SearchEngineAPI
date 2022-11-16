using Octokit.Internal;
using SearchEngineAPI.DBContext;
using SearchEngineAPI.Models.SearchResult;

namespace SearchEngineAPI.Repositories.Favorite
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly SearchEngineContext _searchEngineContext;
        public FavoriteRepository(SearchEngineContext searchEngineContext)
        {
            _searchEngineContext = searchEngineContext;
        }

        public async Task<bool> IsFavoriteExist(SearchResult searchResult,Guid userId)
        {
            var result = _searchEngineContext.Favorite.Any(w => w.GitHubId == searchResult.GitHubId && w.UserId == userId);
            return result;
        }

        public async Task<bool> AddFavorite(Models.Favorite favorite)
        {
            _searchEngineContext.Favorite.Add(favorite);
            _searchEngineContext.SaveChanges();
            return true;
        }

        public async Task<List<Models.Favorite>> GetFavorites(Guid userId)
        {
            var result = _searchEngineContext.Favorite.Where(w => w.UserId == userId).ToList();
            return result;
        }

        public async Task<bool> RemoveFavorite(Guid favoriteId,Guid userId)
        {
            var favorite = _searchEngineContext.Favorite.Where(w => w.FavoriteId == favoriteId && w.UserId == userId).FirstOrDefault();

            if (favorite == null)
                throw new Exception("Favorite is not exist");

            _searchEngineContext.Favorite.Remove(favorite);
            _searchEngineContext.SaveChanges();

            return true;
        }
    }
}
