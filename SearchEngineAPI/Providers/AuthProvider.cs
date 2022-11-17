using SearchEngineAPI.DBContext;
using SearchEngineAPI.Extentions.Auth;
using SearchEngineAPI.Managers;
using SearchEngineAPI.Models.Auth;
using SearchEngineAPI.Models.User;

namespace SearchEngineAPI.Providers
{
    public class AuthProvider : IAuthProvider
    {
        private readonly ILogger<AuthProvider> _logger;
        private readonly SearchEngineContext _searchEngineContext;
        public AuthProvider(SearchEngineContext searchEngineContext, ILogger<AuthProvider> logger)
        {
            _searchEngineContext = searchEngineContext;
            _logger = logger;
        }

        public bool CheckAuthentication()
        {
            throw new NotImplementedException();
        }

        public async Task<User?> Login(AuthUser authUser)
        {
            _logger.LogDebug($"AuthProvider => Login => authUser : {authUser}");
            try
            {
                var user = _searchEngineContext.Users.GetUserAndCheckAuthentication(authUser).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception();
                }
                return user;
            }
            catch(Exception ex)
            {
                _logger.LogError($"AuthProvider => Login => Message : {ex.Message}");
                throw new Exception("Failed to Login");
            }
        }
    }
}
