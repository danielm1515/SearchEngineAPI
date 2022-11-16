namespace SearchEngineAPI.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> CheckAuthorize(string email, string verifyCode);
    }
}
