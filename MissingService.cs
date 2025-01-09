using VISLibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Sqlclient;

namespace VISLibraryManagementSystem.Services
{
    public class MissingBooksService
    {
        private readonly string _connectionString;

        public MissingBooksService(string connectionString)
        {
            // Initialze the connection string for database access
            _connectionString = connectionString;
        }

        // Add a new missing book report
        public void AddMissingBooks(MissingBooksService missingBook)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Get the next MissingBookID
                var missingIdCommand = new SqlCommand("SELECT ISNULL(MAX(MissingId), -1) + 1 FROM MissingBooks", connection);
                var missingId = (int)missingIdCommand.ExecuteScalar();

                var command = new SqlCommand(
                    "INSERT INTO MissingBooks (MissingId, BookId, Title, RepoertedDate, Status" +
                    "VALUES (@MissingId, @BookId, @Title, @ReportedDate, @Status", connection);

                command.Parameters.AddWithValue("@MissingId", missingId);
                command.Parameters.AddWithValue("@BookId", missingBook.BookId);
                command.Parameters.AddWithValue("@Title", missingBook.Title);
                command.Parameters.AddWithValue("@ReportedDate", missingBook.DateTime.Now);
                command.Parameters.AddWithValue("@Status", missingBook.Status);

                command.ExecutenonQuery();
            }
        }

        // Get all missing books
        public List<MissingBooks> GetAllMissing Books()
        {
            var missingBooks = new List<MissingBooks>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM MissingBooks", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) {
                        missingBooks.Add(new MissingBooks
                        {
                            MissingId = reader.GetInt32(0)
                            MissingBookId = reader.GetInt32(1),
                            ReportedDate = reader.GetDateTime(2),
                            Status = reader.GetString(3)
                        });
                    }
                }
            }
            return missingBooks;
        }

        // Get missing book by ID
        public MissingBooks GetMissingBookById(int id)
        {
            MissingBook missingBook = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM MissingBooks WHERE MissingBookId = @MissingBookId", connection);
                command.Parameters.AddWithValue("@MissingBookId", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        missingBook = new MissingBooks
                        {
                            MissingId = reader.GetInt32(0),
                            MissingBookId = reader.GetInt32(1),
                            ReportedDate = reader.GetDateTime(2),
                            Status = reader.GetString(3)
                        };
                    }
                }
            }

            return missingBook;
        }

        // Update the status of a missing book report
        public bool UpdateMissingBookStatus(int id, string status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE MissingBooks SET Status = @Status WHERE MissingBookId = @MissingBookId", connection);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@MissingBookId", id);

                return command.ExecuteNonQuery() > 0;
            }
        }

        // Delete a missing book report
        public bool RemoveMissingBook(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM MissingBooks WHERE MissingBookId = @MissingBookID", connection);
                command.Parameters.AddWithValue("@MissingBookId", id);

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}