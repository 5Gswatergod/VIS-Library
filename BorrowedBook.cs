namespace VISLibraryManagementSystem.Models
{
    public class BorrowedBooks
    {
        public int BorrowId { get; set; }
        public string BookId { get; set; }
        public int StudentID { get; set; }
        public string BorrowDate { get; set; }
        public string DueDate { get; set; }
        public string ReturnDate { get; set; }
        public string Status { get; set; }
    }
}