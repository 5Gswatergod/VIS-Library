using System.ComponentModel.DataAnnotations;

namespace VISLibraryManagementSystem.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PublishedYear { get; set; }
        public int Quantity { get; set; },
        public string OwnerName { get; set; }
    }
}
