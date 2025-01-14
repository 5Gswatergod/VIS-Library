using System;
using System.Collections.Generic;
using VISLMS.Models;

namespace VISLMS
{
    public class VISLMS
    {
        public List<User> Users { get; set; }
        public List<Book> Books { get; set; }
        public List<Account> Accounts { get; private set; }

        public LMS()
        {
            Users = new List<User>();
            Books = new List<Book>();
            Accounts = new List<Account>();
        }

        public bool Login(string username, string password)
        {
            var user = Users.Find(u => u.Username == username && u.Password == password);
            return user != null;
        }

        public void Register(User user)
        {
            if (!Users.Exists(u => u.Username == user.Username))
            {
                Users.Add(user);
            }
            else
            {
                throw new Exception("User already exists");
            }
        }

        public void Logout()
        {
            Console.WriteLine("Logged out");
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public Account GetAccount(string userId)
        {
            return Accounts.Find(a => a.User.UserID == userId);
        }

        public void UpdateAccount(Account updatedAccount)
        {
            var account = Accounts.Find(a => a.User.UserID == updatedAccount.User.UserID);
            if (account != null)
            {
                account.no_borrowed_books = updatedAccount.no_borrowed_books;
                account.no_reserved_books = updatedAccount.no_reserved_books;
                account.no_returned_books = updatedAccount.no_returned_books;
                account.no_lost_books = updatedAccount.no_lost_books;
                account.fine_amount = updatedAccount.fine_amount;
            }
            else
            {
                throw new Exception("Account not found");
            }
        }

        public void DisplaySummary()
        {
            Console.WriteLine("Library Management System Summary:");
            Console.WriteLine($"Total Users: {Users.Count}");
            Console.WriteLine($"Total Books: {Books.Count}");
            Console.WriteLine($"Total Accounts: {Accounts.Count}");
        }
    }
}