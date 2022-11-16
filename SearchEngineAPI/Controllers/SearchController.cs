using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using SearchEngineAPI.Managers;

namespace SearchEngineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IGitHubSearchEngineManager _gitHubSearchEngineManager;
        public SearchController(IGitHubSearchEngineManager gitHubSearchEngineManager)
        {
            _gitHubSearchEngineManager = gitHubSearchEngineManager;
        }

        [HttpGet("Search/{value}")]
        public async Task<IActionResult> Search(string value)
        {
            var result = await _gitHubSearchEngineManager.Search(value);

            return Ok(result);
        }
    }
}
