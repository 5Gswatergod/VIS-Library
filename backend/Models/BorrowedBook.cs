namespace VISLMS.Models
{
    public class BorrowedBooks
    {
        [key]
        public int BorrowID { get; set; }
        public string BookID { get; set; }
        public int StudentID { get; set; }
        public string BorrowDate { get; set; }
        public string DueDate { get; set; }
        public string ReturnDate { get; set; }
        public string Status { get; set; }
    }
}