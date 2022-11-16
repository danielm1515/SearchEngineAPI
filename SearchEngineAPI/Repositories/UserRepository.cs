namespace SearchEngineAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<bool> CheckAuthorize(string email, string verifyCode)
        {
            throw new NotImplementedException();
        }
    }
}
