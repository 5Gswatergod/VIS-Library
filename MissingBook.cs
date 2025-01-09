namespace VISLibraryManagementSystem.Models
{
    public class MissingBook
    {
        public int MissingId { get; set; }
        public string BookId { get; set; }
        public string Title { get; set; }
        public DateTime ReportedDate { get; set; }
        public string Status { get; set; }
    }
}