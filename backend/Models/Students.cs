namespace VISLMS.Models
{
    public class Student : User
    {
        public string Class { get; set; }

        public Student(int userId, string name, string username, string password, string studentClass)
            : base(userId, name, "Student", username, password)
        {
            Class = studentClass;
        }

        public override bool Verify()
        {
            // Add verification logic for Students
            return true;
        }
    }
}