using System;
using System.Windows.Forms;
using VISLibraryManagementSystem.Forms;

namespace VISLibraryManagementSystem
{
    public partial class DashboardForm : Form
    {
        private readonly string _connectionString;

        public DashboardForm(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            ConfigureDashboard();
        }

        // Configure dashboard UI elements
        private void ConfigureDashboard()
        {
            Text = "VIS Library Management System Dashboard";
            btnBookManagement.Click += BtnBookManagement_Click;
            btnStudentManagement.Click += BtnStudentManagement_Click;
            btnExit.Click += BtnExit_Click;
        }

        // Open the Book Management Form
        private void btnBookManagement_Click(object sender, EventArgs e)
        {
            var bookManagementForm = new BookManagementForm(_connectionString);
            bookManagementForm.ShowDialog();
        }

        // Open the Borrow and Return form
        private void BtnBorrowReturn_Click(object sender, EventArgs e)
        {
            var borrowReturnForm = new BorrowReturnForm(_connectionString);
            borrowReturnForm.ShowDialog();
        }

        // Open the Student Management form
        private void BtnStudentManagement_Click(object sender, EventArgs e)
        {
            var studentManagementForm = new StudentManagementForm(_connectionString);
            studentManagementForm.ShowDialog();
        }

        // Exit the application
        private void BtnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}