using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchEngineAPI.Managers.TokenBuilder;
using SearchEngineAPI.Models.Auth;

namespace SearchEngineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenBuilder _tokenBuilder;
        public AuthController(ITokenBuilder tokenBuilder)
        {
            _tokenBuilder = tokenBuilder;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthUser authUser)
        {
            var authToken = await _tokenBuilder.BuildToken(authUser);
            return Ok(authToken);
        }

        [Authorize]
        [HttpGet("CheckAuth")]
        public async Task<IActionResult> CheckAuth()
        {
            return Ok("If You See It You Are Authorized");
        }


    }
}
