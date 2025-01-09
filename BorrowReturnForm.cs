using VISLibraryManagementSystem.Helpers;
using VISLibraryManagementSystem.Services;
using System;
using System.Windows.Forms;

namespace VISLibraryManagementSystem.Forms
{
    public partial class BorrowReturnForm : Form
    {
        private readonly BorrowService _borrowService;
        private readonly BookService _bookService;

        public BorrowReturnForm(string connectionString)
        {
            InitializeComponent();
            _borrowService = new BorrowService(connectionString);
            _bookService = new BookService(connectionString);
            ConfigureDataGridView();
            LoadBorrowedBooks();
        }

        // Configure DataGridView
        private void ConfigureDataGridView()
        {
            ConfigureDataGridViewHelper.onfigureDataGridView(dgvBorrowedBooks);

            var columns = new[]
            {
                DataGridViewHelper.CreateColumn("Borrow ID", "BorrowedBookID", typeof(int)),
                DataGridViewHelper.CreateColumn("Book ID", "BookId", typeof(string)),
                DataGridViewHelper.CreateColumn("Student Name", "StudentID", typeof(string)),
                DataGridViewHelper.CreateColumn("Borrow Date", "BorrowDate", typeof(DateTime)),
                DataGridViewHelper.CreateColumn("Return Date", "ReturnDate", typeof(DateTime), false),
                DataGridViewHelper.CreateColumn("Status", "Status", typeof (string))
            }

            DataGridViewHelper.AddColumns(dgvBorrowedBooks, columns);
        }

        // Load borrowed books into DataGridView
        private void LoadBorrowedBooks()
        {
            var borrowedBooks = _borrowService.GetAllBorrowedBooks();
            DataGridViewHelper.PopulateDataGridView(dgvBorrowedBooks, borrowedBooks);
        }

        // Borrow a book
        private void btnBorrowBook_Click(object sender, EventArgs e)
        {
            var borrowBookForm = new BorrowBookForm(_borrowService, _bookService);
            if (borrowBookForm.ShowDialog() == DialogResult.OK)
            {
                LoadBorrowedBooks();
            }
        }

        // Return a book
        private void btnReturnBook_Click(object sender, EventArgs e)
        {
            var selectedRow = DataGridViewHelper.getSelectedRowData(dgvBorrowedBooks);
            if (selectedRow == null)
            {
                MessageBox.Show("Please select a book to return.", "Return Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var bookId = selectedRow["BookId"].ToString();
            var studentId = Convert.ToInt32(selectedRow["StudentID"]);

            if (_borrowService.ReturnBook(studentId, bookId))
            {
                MessageBox.Show("Book returned successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBorrowedBooks();
            }
            else
            {
                MessageBox.Show("Failed to return the book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}