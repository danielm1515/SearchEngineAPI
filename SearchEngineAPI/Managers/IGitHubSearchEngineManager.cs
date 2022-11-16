using SearchEngineAPI.Models.SearchResult;

namespace SearchEngineAPI.Managers
{
    public interface IGitHubSearchEngineManager
    {
        public Task<List<SearchResult>> Search(string value);
    }
}
