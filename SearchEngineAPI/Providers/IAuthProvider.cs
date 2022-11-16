using SearchEngineAPI.Models.Auth;
using SearchEngineAPI.Models.User;

namespace SearchEngineAPI.Providers
{
    public interface IAuthProvider
    {
        public Task<User> Login(AuthUser authUser);
        public bool CheckAuthentication();
    }
}
