using Octokit;
using SearchEngineAPI.Models.Auth;
using System.Security.Claims;

namespace SearchEngineAPI.Managers.TokenBuilder
{
    public interface ITokenBuilder
    {
        public Task<AuthToken> BuildToken(AuthUser authUser);
        public Task<ClaimsPrincipal?> GetUserByJWT(HttpContext? httpContext);
    }
}
