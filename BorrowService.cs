using VISLibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace VISLibraryManagementSystem.Services
{
    public class BorrowService
    {
        private readonly string _connectionString;

        public BorrowService(string connectionString)
        {
            // Initalize the connection for DB
            _connectionString = connectionString;
        }

        // Borrow a Book
        public bool BorrowBook(int studentId, string bookId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // check if the book is available
                var checkCommand = new SqlCommand("SELECT Availability FROM Books WHERE BookId = @BookId", connection);
                checkCommand.Parameters.AddWithValue("@BookId", bookId);
                var availability = (int?)checkCommand.ExecuteScalar();

                if (availability == null || availability <= 0)
                {
                    return false;
                }

                // Update the availability of the book
                var updateCommand = new SqlCommand("UPDATE Books SET Availability = Availability - 1 WHERE BookID = @BookID", connection);
                updateCommand.Parameters.AddWithValue("@BookId", bookId);
                updateCommand.ExecuteNonQuery();

                // Get the next BorrowedBookId
                var borrowIdCommand = new SqlCommand("SELECT ISNULL(MAX(BorrowId), -1) + 1 FROM BorrowedBooks", connection);
                int nextBorrowId = (int)borrowIdCommand.ExecuteScalar();

                // Insert into BorrowedBooks table
                var insertCommand = new SqlCommand(
                    "INSERT INTO BorrowedBooks (StudentId, BookId, BorrowDate, DueDate, Status) VALUES (@StudentId, @BookId, @BorrowDate, @DueDate, @Status)",
                    connection);

                insertCommand.Parameters.AddWithValue("@BorrowId", nextBorrowId);
                insertCommand.Parameters.AddWithValue("@StudentId", studentId);
                insertCommand.Parameters.AddWithValue("@BookId", bookId);
                insertCommand.Parameters.AddWithValue("@BorrowDate", DateTime.NOW);
                insertCommand.Parameters.AddWithValue("@DueDate", DateTime.Now.AddDays(14));
                insertCommand.Parameters.AddWithValue("@Status", Status);
                insertCommand.ExecuteNonQuery();

                return true;
            }
        }

        // Return a Book
        public bool returnBook(int studentId, int bookId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Update the availability of the book
                var updateCommand = new SqlCommand("UPDATE Books SET Availability = Availability + 1 WHERE BookId = @BookId", connection);
                updateCommand.Parameters.AddWithValue("@BookId", bookId);
                updateCommand.ExecuteNonQuery();

                // Update the BorrowedBooks table
                var updateBorrowCommand = new SqlCommand("UPATE BorrowedBooks SET ReturnDate = @ReturnDate, Status = @Status WHERE BookId = @BookId AND StudentId = @StudentId AND Status = @CurrrentStatus", connection);

                updateCommand.Parameters.AddWithValue("@ReturnDate", DateTime.NOW);
                updateCommand.Parameters.AddWithValue("@Status", "Returned");
                updateCommand.Parameters.AddWithValue("@BookId", bookId);
                updateCommand.Parameters.AddWithValue("@StudentId", studentId);
                updateCommand.Parameters.AddWithValue("@CurrrentStatus", "Borrowed");

                return updateBorrowCommand.ExecuteNonQuery() > 0;
            }
        }

        // Get All Borrowed Books
        public List<BorrowedBooks> GetAllBorrowedBooks()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM BorrowedBooks", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        borrowedBooks.Add(new BorrowedBooks
                            {
                                BorrowId = reader.GetInt32(0),
                                StudentId = reader.GetInt32(1),
                                BookId = reader.GetString(2),
                                BorrowDate = reader.GetDateTime(3),
                                DueDate = reader.GetDateTime(4),
                                ReturnDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                Status = reader.GetString(6)
                            }
                        );
                    }
                }
            }

            return borrowedBooks;
        }

        // Get borrowed books by a student ID
        public List<BorrowedBook> GetBorrowedBooksByStudentId(int studentId)
        {
            var borrowedBooks = new List<BorrowedBook>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM BorrowedBooks WHERE StudentId = @StudentId", connection);
                command.Parameters.AddWithValue("@StudentId", studentId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        borrowedBooks.Add(new BorrowedBook
                        {
                            BorrowedBookId = reader.GetInt32(0),
                            StudentId = reader.GetInt32(1),
                            BookId = reader.GetString(2),
                            BorrowDate = reader.GetDateTime(3),
                            DueDate = reader.GetDateTime(4),
                            ReturnDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                            Status = reader.GetString(6)
                        });
                    }
                }
            }

            return borrowedBooks;
        }
    }
}