namespace SearchEngineAPI.Models.SearchResult
{
    public class SearchResult
    {
        public long GitHubId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Url { get; set; }
        public string OwnerName { get; set; }
        public string OwnerUrl { get; set; }
        public bool IsFavorite { get; set; } = false;
    }
}
