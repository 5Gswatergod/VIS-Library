using System.ComponentModel.DataAnnotations;

namespace VISLMS.Models
{
    public abstract class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(int userId, string name, string userType, string username, string password)
        {
            UserId = userId;
            Name = name;
            UserType = userType;
            Username = username;
            Password = password;
        }

        public abstract bool Verify();
    }
