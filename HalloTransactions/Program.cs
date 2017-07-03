using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace HalloTransactions
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=.;Initial Catalog=NORTHWND;Integrated Security=True";

            var employees = new List<(int id, string firstname, DateTime birthdate)>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id=EmployeeId, Name=Firstname, Date=Birthdate FROM Employees";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = (int)reader["Id"];
                            var name = (string)reader["Name"];
                            var date = (DateTime)reader["Date"];
                            employees.Add((id, name, date));
                        }
                    }
                }

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.id,2} - {e.firstname,-10} | {e.birthdate.ToString("dd.MMMM yyyy", new CultureInfo("es-ES"))}");
                }
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        foreach (var e in employees)
                        {
                            if (e.id == 5)
                                throw new Exception("Id was 5. :O");

                            using (var command = connection.CreateCommand())
                            {
                                command.Transaction = transaction;
                                command.CommandText = "UPDATE Employees SET Birthdate = @date WHERE EmployeeId = @id";
                                command.Parameters.AddWithValue("@id", e.id);
                                command.Parameters.AddWithValue("@date", e.birthdate.AddYears(-1));

                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                    }
                }
            }

            Console.WriteLine("Fertig.");
            Console.ReadKey();
        }
    }
}
