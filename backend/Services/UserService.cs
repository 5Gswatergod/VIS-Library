using VISLMS.Models;
using System.Collections.Generic;

namespace VISLMS.Services
{
    public class UserService
    {
        private List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public bool VerifyUser(int userId)
        {
            var user = _users.Find(u => u.Id == userId);
            return user?.Verify() ?? false;
        }

        public string GetAccountDetails(int userId)
        {
            var user = _users.Find(u => u.Id == userId);
            return user?.CheckAccount() ?? "User not found";
        }
    }
}
