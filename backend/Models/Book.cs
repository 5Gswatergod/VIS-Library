using System.ComponentModel.DataAnnotations;

namespace VISLMS.Models
{
    public class Book
    {
        public string BookID { get; set; }         // ISBN of the book
        public string Title { get; set; }        // Title of the book
        public string Author { get; set; }       // Author of the book
        public int PublishedYear { get; set; }  // Publication details of the book
        public string Genre { get; set; }       // Genre of the book
        public int Quantity { get; set; }       // Number of copies available
        public string OwnerName { get; set; }   // Name of the owner

        // Method to display due date
        public DateTime ShowDueDate()
        {
            return DateTime.Now.AddDays(14); // Example: 2 weeks from now
        }

        // Method to handle reservation status
        public string ReservationStatus()
        {
            return "Not Reserved"; // Placeholder logic for reservation status
        }

        // Method for feedback on the book
        public string Feedback()
        {
            return "No feedback available."; // Placeholder logic for feedback
        }

        // Method for book request
        public string BookRequest()
        {
            return "Book request submitted."; // Placeholder logic for book request
        }

        // Method to renew book information
        public string RenewInfo()
        {
            return "Book renewal successful."; // Placeholder logic for book renewal
        }
    }
}
