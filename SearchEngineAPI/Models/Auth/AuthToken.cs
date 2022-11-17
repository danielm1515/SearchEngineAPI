namespace SearchEngineAPI.Models.Auth
{
    public class AuthToken
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
