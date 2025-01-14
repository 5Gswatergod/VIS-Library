using VISLMS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace VISLMS.Services
{
    public class BookService
    {
        private readonly string _connectionString;

        public BookService(string connectionString)
        {
            // Initalize the connection for DB access
            _connectionString = connectionString;
        }

        // Get all books
        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Books", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            BookId = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Genre = reader.GetString(3),
                            PublicationYear = reader.GetInt32(4),
                            Availability = reader.GetInt32(5)
                        });
                    }
                }
            }

            return books;
        }

        // Get book by ID
        public Book GetBookById(string bookId)
        {
            Book book = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Books WHERE BookId = @BookId", connection);
                command.Parameters.AddWithValue("@BookId", bookId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        book = new Book
                        {
                            BookId = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Genre = reader.GetString(3),
                            PublicationYear = reader.GetInt32(4),
                            Availability = reader.GetInt32(5)
                        }
                    }
                }
            }

            return book;
        }

        // Add a new book
        public void AddBook(Book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                                    "INSERT INTO Books (Title, Author, Genre, PublicationYear, Availability)" +
                                    "VALUES (@Title, @Author, @Genre, @PublicationYear, @Availability)", connection);

                command.Parameters.AddWithValue("@BookId", book.BookId);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Genre", book.Genre);
                command.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
                command.Parameters.AddWithValue("@Availability", book.Availability);

                command.ExecuteNonQuery();
            }
        }

        // Update a book details
        public void UpdateBook(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                                  "UPDATE Books SET Title=@Title, Author=@Author, Genre=@Genre, PublicationYear=@PublicationYear, Availability=@Availability" +
                                  " WHERE BookId=@BookId", connection);

                command.Parameters.AddWithValue("@BookId", book.BookId);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Genre", book.Genre);
                command.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
                command.Parameters.AddWithValue("@Availability", book.Availability);

                return command.ExecuteNonQuery() > 0;
            }
        }

        // Delete a book
        public bool DeleteBook(string bookId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Books WHERE BookId = @BookId", connection);
                command.Parameters.AddWithValue("@BookId", bookId);

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}