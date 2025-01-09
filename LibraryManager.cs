using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class BookManager
{
    private string connectionString = "VISLibraryDB";

    // Function to add book to the database
    public bool AddBook(string bookId, string title, string author, int publishedYear, string genre, string status)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Books (book_Id, title, author, published_year, genre, status" + "VALUES (@book_id, @title, @author, @@published_year, @genre, @status)";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@book_id", bookId);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@author", author);
                    command.Parameters.AddWithValue("@published_year", publishedYear);
                    command.Parameters.AddWithValue("@genre", genre);
                    command.Parameters.AddWithValue("@available_copies", availableCopies);
                    command.Parameters.AddWithValue("@status", status);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }
}

//Function to delete book from the database
public bool DeleteBook(string bookId){    }