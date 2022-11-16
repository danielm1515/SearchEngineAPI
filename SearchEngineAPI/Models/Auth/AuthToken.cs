namespace SearchEngineAPI.Models.Auth
{
    public class AuthToken
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
