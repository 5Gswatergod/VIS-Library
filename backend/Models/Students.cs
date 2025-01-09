using System.ComponentModel.DataAnnotations;

namespace VISLibraryManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
    }
}