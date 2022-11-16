using SearchEngineAPI.DBContext;
using SearchEngineAPI.Extentions.Auth;
using SearchEngineAPI.Models.Auth;
using SearchEngineAPI.Models.User;

namespace SearchEngineAPI.Providers
{
    public class AuthProvider : IAuthProvider
    {

        private readonly SearchEngineContext _searchEngineContext;
        public AuthProvider(SearchEngineContext searchEngineContext)
        {
            _searchEngineContext = searchEngineContext;
        }

        public bool CheckAuthentication()
        {
            throw new NotImplementedException();
        }

        public async Task<User?> Login(AuthUser authUser)
        {
            var user = _searchEngineContext.Users.GetUserAndCheckAuthentication(authUser).FirstOrDefault();
            if (user == null)
            {
                throw new Exception();
            }
            return user;
        }
    }
}
