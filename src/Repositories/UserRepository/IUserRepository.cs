using Multilang.Models.Accounts;

namespace Multilang.Repositories.UserRepository {

    public interface IUserRepository
    {
        User GetUserById(string id);
        void UpdateFirebaseToken(string id, string token);
    }
}