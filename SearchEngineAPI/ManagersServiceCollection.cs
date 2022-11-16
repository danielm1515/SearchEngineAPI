using Microsoft.AspNetCore.Identity;
using Octokit;
using SearchEngineAPI.Managers;
using SearchEngineAPI.Managers.Favorite;
using SearchEngineAPI.Managers.TokenBuilder;
using SearchEngineAPI.Providers;
using SearchEngineAPI.Repositories.Favorite;

namespace SearchEngineAPI
{
    public class ManagersServiceCollection
    {
        private readonly IServiceCollection _services;
        public ManagersServiceCollection(IServiceCollection services)
        {
            _services = services;
            _services.AddScoped<IFavoriteManager, FavoriteManager>();
            _services.AddScoped<IGitHubSearchEngineManager, GitHubSearchEngineManager>();
            _services.AddScoped<ITokenBuilder, TokenBuilder>();
            _services.AddScoped<IAuthProvider, AuthProvider>();

            _services.AddScoped<IFavoriteRepository, FavoriteRepository>();
        


        }
    }
}
