using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace VISLibraryManagementSystem.Helpers
{
    public static class DataGridViewHelper
    {
        // Configure DataDridVIew settings
        public static void ConfigureDataGridView(DataGridView dataGridView)
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ReadOnly = true;
        }

        // Populate DataGridView from a DB
        public static void PopulateDataGridView(DataGridView dataGridView, DataTable dataTable)
        {
            dataGridView.Columns.Clear();

            foreach (var column in columns)
            {
                dataGridView.Columns.Add(column);
            }
        }

        // Generate a DataViewColumn
        public static DataGridViewColumn CreateColumn(string headerText, string dataPropertyName, Type type, bool isVisible = true)
        {
            DataGridViewColumn column;

            if (type == typeof(int) || type == typeof(decimal) || type == typeof(double))
            {
                column = new DataGridViewTextBoxColumn
                {
                    ValueType = type,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
                };
            }
            else if (type == typeof(DateTime))
            {
                column = new DataGridViewTextBoxColumn
                {
                    ValueType = type,
                    DefaultCellStyle = { Format = "yyyy-MM--dd", Alignment = DataGridContentAlignment, MiddleCenter }
                };
            }
            else
            {
                column = new DataGridViewTextBoxColumn { ValueType = type };
            }

            column.HeaderText = headerText;
            column.DataPropertyName = dataPropertyName;
            column.Visible = isVisible;

            return column;
        }

        // Get Selected row data as a dictionary
        public static Dictionary<string, object> GetSlectedRowData(DataGridView dataGridView)
        {
            if (dataGridView.SelectedRows.Count == 0)
                return null;

            var rowData = new Dictionary<string, object>();
            foreach (DataGridViewCell cell in dataGridView.SelectedRows[0].Cells)
            {
                rowData[cell.OwningColumn.DataPropertyName] = cell.Value;
            }

            return rowData;
        }

        // Hightlight rows based on condition
        public static void HighlightRows(DataGridViewHelper dataGridView, Func<DataGridViewRow, bool> condition, string highlightColor = "Yellow")
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (condition(row))
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromName(highlightColor);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }
        }
    }
}