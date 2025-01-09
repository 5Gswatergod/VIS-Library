// StudentReportForm.cs: Form for generating and viewing student reports

using VISLibraryManagementSystem.Helpers;
using VISLibraryManagementSystem.Models;
using VISLibraryManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VISLibraryManagementSystem.Forms
{
    public partial class StudentReportForm : Form
    {
        private readonly StudentService _studentService;
        private readonly BorrowService _borrowService;

        public StudentReportForm(string connectionString)
        {
            InitializeComponent();
            _studentService = new StudentService(connectionString);
            _borrowService = new BorrowService(connectionString);
            ConfigureDataGridView();
        }

        // Configure DataGridView for student reports
        private void ConfigureDataGridView()
        {
            DataGridViewHelper.ConfigureDataGridView(dgvStudentReport);

            var columns = new[]
            {
                DataGridViewHelper.CreateColumn("Student ID", "StudentId", typeof(int)),
                DataGridViewHelper.CreateColumn("Name", "Name", typeof(string)),
                DataGridViewHelper.CreateColumn("Email", "Email", typeof(string)),
                DataGridViewHelper.CreateColumn("Books Borrowed", "BooksBorrowed", typeof(int))
            };

            DataGridViewHelper.AddColumns(dgvStudentReport, columns);
        }

        // Load student data with borrow statistics into the DataGridView
        private void LoadStudentData()
        {
            var students = _studentService.GetAllStudents();
            var borrowings = _borrowService.GetAllBorrowedBooks();

            var studentReports = new List<dynamic>();

            foreach (var student in students)
            {
                var booksBorrowedCount = borrowings.FindAll(b => b.StudentID == student.StudentID && b.Status == "Borrowed").Count;

                studentReports.Add(new
                {
                    StudentID = student.StudentID,
                    Name = student.Name,
                    Email = student.Email,
                    BooksBorrowed = booksBorrowedCount
                });
            }

            DataGridViewHelper.PopulateDataGridView(dgvStudentReport, studentReports);
        }

        // Generate student report
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                LoadStudentData();
                MessageBox.Show("Student report generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to generate report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Close the report form
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
