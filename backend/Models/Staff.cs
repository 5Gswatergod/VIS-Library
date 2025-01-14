using System.ComponentModel.DataAnnotations;

namespace VISLMS.Models
{
    public class Staff : User
    {
        public string Department { get; set; }

        public Staff(int userId, string name, string username, string password, string department) : base(userId, name, "Staff", username, password)
        {
            Department = department;
        }

        public override bool Verify()
        {
            return true;
        }
    }
}