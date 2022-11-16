using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchEngineAPI.Managers.Favorite;
using SearchEngineAPI.Managers.TokenBuilder;
using SearchEngineAPI.Models.SearchResult;

namespace SearchEngineAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly ITokenBuilder _tokenBuilder;
        private readonly IFavoriteManager _favoriteManager;
        public FavoriteController(ITokenBuilder tokenBuilder, IFavoriteManager favoriteManager)
        {
            _tokenBuilder = tokenBuilder;
            _favoriteManager = favoriteManager;
        }

        [HttpGet("GetFavorites")]
        public async Task<IActionResult> GetFavorites()
        {
            var principal = await _tokenBuilder.GetUserByJWT(this.HttpContext);
            var userId = Guid.Parse(principal?.FindFirst("UserId")?.Value);
            var results = await _favoriteManager.GetFavorites(userId);

            return Ok(results);
        }

        [HttpDelete("RemoveFavorite/{favoriteId}")]
        public async Task<IActionResult> RemoveFavorite(Guid favoriteId)
        {
            var principal = await _tokenBuilder.GetUserByJWT(this.HttpContext);
            var userId = Guid.Parse(principal?.FindFirst("UserId")?.Value);
            var results = await _favoriteManager.RemoveFavorite(favoriteId,userId);

            return Ok(results);
        }

        [HttpPost("AddFavorite")]
        public async Task<IActionResult> AddFavorite(SearchResult searchResult)
        {
            var principal = await _tokenBuilder.GetUserByJWT(this.HttpContext);
            var userId = Guid.Parse(principal?.FindFirst("UserId")?.Value);
            var results = await _favoriteManager.AddFavorite(searchResult, userId);

            return Ok(results);
        }
    }
}
