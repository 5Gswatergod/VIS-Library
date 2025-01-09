using System.ComponentModel.DataAnnotations;

namespace VISLibraryManagementSystem.Models
{
    public class MissingBook
    {
        [key]
        public int MissingID { get; set; }
        public string BookID { get; set; }
        public string Title { get; set; }
        public DateTime ReportedDate { get; set; }
        public string Status { get; set; }
    }
}