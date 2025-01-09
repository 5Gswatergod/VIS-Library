using VISLibraryManagementSystem,Helpers;
using VISLibraryManagementSystem.Models;
using VISLibraryManagementSystem.Services;
using System;
using System.Windows.Forms;

namespace VISLibraryManagementSystem.Forms
{
    public partial class StudentManagementSystem : Form
    {
        private readonly StudentService _studentService;

        public StudentManagementSystem(string connectionString)
        {
            InitializeComponent();
            _studentService = new StudentService(connectionString);
            ConfigureDataGridView();
            LoadStudents();
        }

        // Configure DataGridView for students
        private void ConfigureDataGridView()
        {
            DataGridViewHelper.ConfigureDataGridView(dgvStudents);

            var columns = new[]
            {
                DataGridViewHelper.CreateColumn("Student ID", "StudentId", typeof(int)),
                DataGridViewHelper.CreateColumn("Name", "Name", typeof(string)),
                DataGridViewHelper.CreateColumn("Address", "Address", typeof(string)),
                DataGridViewHelper.CreateColumn("Email", "Email", typeof(string))
            };

            DataGridViewHelper.AddColumns(dgvStudents, columns);
        }

        // Load students into DataGridView
        private void LoadStudents()
        {
            var students = _studentService.GetAllStudents();
            DataGridViewHelper.PopulateDataGridView(dgvStudents, students);
        }

        // Add a new student
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            var addStudentForm = new AddStudentForm(_studentService);
            if (addStudentForm.ShowDialog() == DialogResult.OK)
            {
                LoadStudents();
            }
        }

        // Edit a selected student
        private void btnEditStudent_Click(object sender, EventArgs e)
        {
            var selectedRow = DataGridViewHelper.GetSelectedRowData(dgvStudents);
            if (selectedRow == null)
            {
                MessageBox.Show("Please select a student to edit.", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var studentId = Convert.ToInt32(selectedRow["StudentId"]);
            var student = _studentService.GetStudentById(studentId);

            var editStudentForm = new EditStudentForm(_studentService, student);
            if (editStudentForm.ShowDialog() == DialogResult.OK)
            {
                LoadStudents();
            }

            // Delete selected student
            private void btnDeleteStudent_Click(object sender, EventArgs e)
            {
                var selectedRow = DataGridViewHelper.GetSelectedRowData(dgvStudents);
                if (selectedRow == null)
                {
                    MessageBox.Show("Please select a student to delete.", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var studentId = Convert.ToInt32(selectedRow["StudentId"]);

                var result = MessageBox.Show("Are you sure you want to delete this student?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (_studentService.DeleteStudent(studentId))
                    {
                        MessageBox.Show("Student deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStudents();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}