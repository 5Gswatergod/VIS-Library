using VISLibraryManagementSystem.Helpers;
using VISLibraryManagementSystem.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace VISLibraryManagementSystem.Forms
{
    public partial class BookManagementForm : Form
    {
        private readonly BookService _bookService;

        public BookManagementForm(string connectionString)
        {
            InitializeComponent();
            _bookService = new BookService(connectionString);
            ConfigureDataGridview();
            LoadBooks();
        }

        // COnfigure DataGrid View
        private void ConfigureDataGridview()
        {
            DataGridViewHelper.ConfigureDataGridView(dgvBooks);

            var columns = new[]
            {
                DataGridViewHelper.CreateColumn("Book ID", "BookId", typeof(string)),
                DataGridViewHelper.CreateColumn("Title", "Title", typeof(string)),
                DataGridViewHelper.CreateColumn("Author", "Author", typeof(string)),
                DataGridViewHelper.CreateColumn("Publisher", "Publisher", typeof(string)),
                DataGridViewHelper.CreateColumn("Publication Year", "PublicationYear", typeof(int)),
                DataGridViewHelper.CreateColumn("Availability", "Availability", typeof(int))
            };

            DataGridViewHelper.AddColumns(dgvBooks, columns);
        }

        // Load books from DataGridView
        private void LoadBooks()
        {
            var books = _bookService.GetAllBooks();
            DataGridViewHelper.PopulateDataGridView(dvgBooks, books);
        }

        // Add a new book
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            var addBookForm = new AddBookForm(_bookService);
            if (addBookForm.ShowDialog() == DialogResult.OK)
            {
                LoadBooks();
            }
        }

        // Edit a selected book
        private void btnEditBook_Click(object sender, EventArgs e)
        {
            var selectedRow = DataGridViewHelper.GetSelectedRowData(dgvBooks);
            if (selectedRow == null)
            {
                MessageBox.show("Please select a book to edit", "Edit Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var editBookForm = new EditBookForm(_bookService, selectedRow);
            if (editBookForm.ShowDialog() == DialogResult.OK)
            {
                LoadBooks();
            }
        }

        // Delete a selected book
        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            var selectedRow = DataGridViewHelper.GetSelectedRowData(dgvBooks);
            if (selectedRow == null)
            {
                MessageBox.Show("Please select a book to delete", "Delete Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var bookId = selectedRow["BookId"].ToString();
            var confirmResult = MessageBox.show($"Are you sure you want to delete the book with ID: {BookId}?", "Confirm Delete", MessageBoxbuttons.YesNo, MessageBoxIcon.Question);
            
            if (confirmResult == DialogResult.Yes)
            {
                if (_bookService.DeleteBook(bookId))
                {
                    MessageBox.Show("Book deleted Sucessfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBooks();
                }
                else
                {
                    MessageBox.Show("Failed to delete the book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
}