using SearchEngineAPI.Models.Auth;
using SearchEngineAPI.Models.User;

namespace SearchEngineAPI.Extentions.Auth
{
    public static class AuthFilterExtensions
    {
        public static IQueryable<User> GetUserAndCheckAuthentication(this IQueryable<User> query, AuthUser authUser)
        {
            return query.Where(w => w.Email == authUser.Email && w.Password == authUser.Password);
        }
    }
}
