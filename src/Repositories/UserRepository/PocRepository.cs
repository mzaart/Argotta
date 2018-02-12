using Multilang.Models.Db;
using System;
using System.Collections.Generic;

namespace Multilang.Repositories.UserRepository {

    public class UserRepository : IUserRepository
    {
        private static int maxId = 0;
        
        private List<User> users;

        public UserRepository()
        {
            this.users = new List<User>();
        }

        public User FindUser(Predicate<User> predicate)
        {
            return users.Find(predicate);
        }

        public bool UserExists(string displayName)
        {
            return users.Find(u => u.displayName == displayName) != null;
        }

        public bool AddToBlockedUsers(string userId, string blockedId)
        {
            User u = GetUserById(userId);
            if (u == null)
            {
                return false;
            }

            u.blockedIds.Add(blockedId);
            return true;
        }

        public bool AddUser(User user)
        {
            user.id = maxId++.ToString();
            users.Add(user);
            return true;
        }

        public User GetUserById(string id)
        {
            return users.Find(u => u.id == id);
        }

        public bool RemoveFromBlockedUsers(string userId, string blockedId)
        {
            User u = GetUserById(userId);
            if (u == null)
            {
                return false;
            }

            string id = u.blockedIds.Find(i => i == blockedId);
            if (id == null)
            {
                return false;
            }

            u.blockedIds.Remove(blockedId);
            return true;
        }

        public bool SetFirebaseToken(string id, string token)
        {
            User u = GetUserById(id);
            if (u == null)
            {
                return false;
            }

            u.firebaseToken = token;
            return true;
        }

        public bool SetLanguage(string id, string language)
        {
            User u = GetUserById(id);
            if (u == null)
            {
                return false;
            }

            u.language = language;
            return true;
        }

        public bool UpdateDisplayName(string id, string displayName)
        {
            User u = GetUserById(id);
            if (u == null)
            {
                return false;
            }

            u.displayName = displayName;
            return true;
        }

        public bool UpdatePassword(string id, string password)
        {
            User u = GetUserById(id);
            if (u == null)
            {
                return false;
            }

            u.passwordHash = password;
            return true;
        }

        public bool DeleteUser(string id)
        {
            User u = GetUserById(id);
            if (u == null)
            {
                return false;
            }
            else
            {
                users.Remove(u);
                return true;
            }
        }
    }
}