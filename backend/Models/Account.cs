using System.ComponentModel.DataAnnotations;

namespace VISLMS.Models
{
    public class Account
    {
        public int AccountID { get; set; }        // Unique ID for the account
        public int UserID { get; set; }           // ID of the associated user
        public int NoBorrowedBooks { get; set; }  // Number of borrowed books
        public int NoReservedBooks { get; set; }  // Number of reserved books
        public int NoReturnedBooks { get; set; }  // Number of returned books
        public int NoLostBooks { get; set; }      // Number of lost books
        public decimal FineAmount { get; set; }   // Total fine amount

        // Method to calculate fine
        public decimal CalculateFine()
        {
            return FineAmount;
        }
    }
}
