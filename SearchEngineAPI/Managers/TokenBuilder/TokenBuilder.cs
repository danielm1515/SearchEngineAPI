using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SearchEngineAPI.Models.Auth;
using SearchEngineAPI.Providers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SearchEngineAPI.Managers.TokenBuilder
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly IAuthProvider _authProvider;
        public TokenBuilder(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }
        public async Task<AuthToken> BuildToken(AuthUser authUser)
        {
            var user = await _authProvider.Login(authUser);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_application_api_secret"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expirationDate = DateTime.UtcNow.AddMonths(1);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, authUser.Email.ToString()),
                new Claim(ClaimTypes.Email,authUser.Email.ToString()),
                new Claim("UserId",user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(audience: "searchAudience",
                                              issuer: "searchIssuer",
                                              claims: claims,
                                              expires: expirationDate,
                                              signingCredentials: credentials);

            var authToken = new AuthToken();
            authToken.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authToken.ExpirationDate = expirationDate;
            authToken.Email = user.Email;

            return authToken;

        }

        public async Task<ClaimsPrincipal?> GetUserByJWT(HttpContext? httpContext)
        {
            var token = httpContext?.Request.Headers.Authorization;
            var cleanJwt = token.Value.ToString().Split(' ')[1];
            var principal = ValidateToken(cleanJwt);
            return principal;
        }


        public ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = "searchAudience";
            validationParameters.ValidIssuer = "searchIssuer";
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_application_api_secret"));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);


            return principal;
        }
    }
}
