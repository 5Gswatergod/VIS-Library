using VISLMS.Helpers;
using VISLMS.Models;
using VISLMS.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VISLMS.Forms
{
    public partial class BorrowingReportForm : Form
    {
        private readonly BorrowService _borrowService;

        public BorrowingReportForm(string connectionString)
        {
            InitializeComponent();
            _borrowService = new BorrowService(connectionString);
            ConfigureDataGridView();
        }

        // Configure DataGridView for borrowing reports
        private void ConfigureDataGridView()
        {
            DataGridViewHelper.ConfigureDataGridView(dgvBorrowingReport);

            var columns = new[]
            {
                DataGridViewHelper.CreateColumn("Borrow ID", "BorrowedBookID", typeof(int)),
                DataGridViewHelper.CreateColumn("Book ID", "BookID", typeof(string)),
                DataGridViewHelper.CreateColumn("Student ID", "StudentID", typeof(int)),
                DataGridViewHelper.CreateColumn("Borrow Date", "BorrowDate", typeof(DateTime)),
                DataGridViewHelper.CreateColumn("Return Date", "ReturnDate", typeof(DateTime?)),
                DataGridViewHelper.CreateColumn("Status", "Status", typeof(string))
            };

            DataGridViewHelper.AddColumns(dgvBorrowingReport, columns);
        }

        // Load borrowing data into the DataGridView
        private void LoadBorrowingData()
        {
            var borrowingData = _borrowService.GetAllBorrowedBooks();
            DataGridViewHelper.PopulateDataGridView(dgvBorrowingReport, borrowingData);
        }

        // Generate report based on filter
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                var filterStartDate = dtpStartDate.Value.Date;
                var filterEndDate = dtpEndDate.Value.Date;

                if (filterStartDate > filterEndDate)
                {
                    MessageBox.Show("Start date cannot be later than end date.", "Invalid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var filteredData = FilterBorrowingData(filterStartDate, filterEndDate);
                DataGridViewHelper.PopulateDataGridView(dgvBorrowingReport, filteredData);

                MessageBox.Show("Report generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to generate report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Filter borrowing data by date range
        private List<BorrowedBook> FilterBorrowingData(DateTime startDate, DateTime endDate)
        {
            var allBorrowings = _borrowService.GetAllBorrowedBooks();
            return allBorrowings.FindAll(borrowing => borrowing.BorrowDate.Date >= startDate && borrowing.BorrowDate.Date <= endDate);
        }

        // Close the report form
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
