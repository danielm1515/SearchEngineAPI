using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using SearchEngineAPI.Managers;
using SearchEngineAPI.Managers.TokenBuilder;

namespace SearchEngineAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ITokenBuilder _tokenBuilder;
        private readonly IGitHubSearchEngineManager _gitHubSearchEngineManager;
        public SearchController(IGitHubSearchEngineManager gitHubSearchEngineManager, ITokenBuilder tokenBuilder)
        {
            _gitHubSearchEngineManager = gitHubSearchEngineManager;
            _tokenBuilder = tokenBuilder;
        }

        [HttpGet("Search/{value}")]
        public async Task<IActionResult> Search(string value)
        {
            var principal = await _tokenBuilder.GetUserByJWT(this.HttpContext);
            var userId = Guid.Parse(principal?.FindFirst("UserId")?.Value);
            var result = await _gitHubSearchEngineManager.Search(value, userId);

            return Ok(result);
        }
    }
}
