using System;
using System.Data;
using System.Data.SqlClient;

namespace VISLMS.Utilities
{
    public class DatabaseHelper
    {
        // Execute a SQL command that does not return any result
        public static int ExecuteNonQuery(string connectionString, string commandText, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(commandText, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        // Execute a SAL command that return a single value
        public static object ExecuteScalar(string connectionString, string commandText, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(connection))
            using (var command = new SqlCommand(commandText, connection))
            {
                command.Parameters.AddRange(parameters);
            }

            connection.Open();
            return command.ExecuteScalar();
        }

        // Execute a SQL command that returns a DataTable
        public static DataTable ExecuteQuery(string connectionString, string commandText, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(commandText, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        // Create a SQL parameter
        public static SqlParameter CreateParameter(string parameterName, object value, SqlDbType dbType)
        {
            return new SqlParameter
            {
                ParameterName = parameterName,
                Value = value ?? DBNull.Value,
                SqlDbType = dpType
            };
        }
    }
}