// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Program
{
    static string connectionString = "Server=DESKTOP-4PCU5RA\\SQLEXPRESS;Database=Bookshop;Integrated Security=True;TrustServerCertificate=True;";
    static void Main()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                decimal totalPrices = CalculateTotalPrices(connection);
                Console.WriteLine($"Сумма цен всех книг: {totalPrices}");
                int totalPages = CalculateTotalPages(connection);
                Console.WriteLine($"Сумма страниц всех книг: {totalPages}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static decimal CalculateTotalPrices(SqlConnection connection)
    {
        string query = "SELECT SUM(Price) FROM BooksDetails";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            object result = command.ExecuteScalar();

            if (result != DBNull.Value)
            {
                return Convert.ToDecimal(result);
            }

            return 0;
        }
    }

    static int CalculateTotalPages(SqlConnection connection)
    {
        string query = "SELECT SUM(Quantity) FROM BooksDetails";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            object result = command.ExecuteScalar();

            if (result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }

            return 0;
        }
    }
}
