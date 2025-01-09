using VISLibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace VISLibraryManagementSystem.Services
{
    public class StudentService
    {
        private readonly string _connectionString;

        public StudentService(string connectionString)
        {
            // Initialize the connection string for database access
            _connectionString = connectionString;
        }

        // Add a new student
        public bool AddStudent(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("INSERT INTO Students (StudentId, Name, Email) VALUES (@StudentId, @Name, @Email)", connection);

                command.Parameters.AddWithValue("@StudentId", student.StudentId);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Email", student.Email);

                return command.ExecuteNonQuery() > 0;
            }
        }

        // Get a student by ID
        public Student Get StudentById(int studentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM Students WHERE StudentId = @StudentId", connection);
                command.Parameters.AddWithValue("@StudentId", studentId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Student
                        {
                            StudentId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            JoinDate = reader.GetDateTime(3).ToString("yyyy-MM-dd")
                        };
                    }
                }
            }

            return null;
        }

        // Get all students
        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Students WHERE Student", connection);

                using (var reader = command.ExecuteReader())
                {
                    students.Add(
                        new Student
                        {
                            StudentId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            JoinDate = reader.GetDateTime(3).ToString("yyyy-MM-dd")
                        });
                }
            }

            return students;
        }

        // Update a student's information
        public bool UpdateStudent(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("UPDATE Students SET Name = @Name, Email = @Email WHERE StudentId = @StudentId", connection);

                command.Parameters.AddWithValue("@StudentId", student.StudentId);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Email", student.Email);

                return command.ExecuteNonQuery() > 0;
            }
        }

        // Delete a student by ID
        public bool DeleteStudent(int studentId)
        {
            using (var connection = new SqlConnection(_connectionString)
            {
                connection.Open();

                var command = new SqlCommand("DELETE FROM Students WHERE StudentId = @StudentId", connection);
                command.Parameters.AddWithValue("@StudentId", studentId);

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}