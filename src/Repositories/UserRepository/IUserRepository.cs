using System;
using Multilang.Models.Db;

namespace Multilang.Repositories.UserRepository {

    public interface IUserRepository
    {
        bool UserExists(string displayName);
        User FindUser(Predicate<User> predicate);
        bool AddUser(User user);
        User GetUserById(string id);
        bool SetFirebaseToken(string id, string token);
        bool AddToBlockedUsers(string userId, string blockedId);
        bool RemoveFromBlockedUsers(string userId, string blockedId);
        bool SetLanguage(string id, string language);
        bool UpdateDisplayName(string id, string displayName);
        bool UpdatePassword(string id, string password);
        bool DeleteUser(string id);
    }
}