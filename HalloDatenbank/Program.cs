using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HalloDatenbank
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://www.connectionstrings.com/
            var connectionString = "Data Source=.;Initial Catalog=NORTHWND;Integrated Security=True";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Skalare Werte von der DB lesen
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT Count(*) FROM Employees";

                        var result = (int)command.ExecuteScalar();

                        Console.WriteLine($"{result} Employees in database.");
                    }

                    var employees = new List<Employee>();
                    using (var command = connection.CreateCommand())
                    {
                        Console.Write("Geben Sie bitte einen Namen ein: ");
                        var input = Console.ReadLine();
                        command.CommandText = $"SELECT * FROM Employees WHERE Firstname like @search + '%'";

                        //var p = new SqlParameter();
                        //var p = command.CreateParameter();
                        //p.ParameterName = "@search";
                        //p.Value = input;
                        //command.Parameters.Add(p);
                        command.Parameters.AddWithValue("@search", input);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var e = new Employee();
                                e.Id = (int)reader["EmployeeId"];
                                e.Firstname = reader.GetString(2);
                                e.Lastname = (string)reader["Lastname"];

                                e.Birthdate = reader.GetDateTime(5);
                                e.Hiredate = (DateTime)reader["Hiredate"];

                                Console.WriteLine($"{e.Firstname,10} {e.Lastname,-10} - [{e.Birthdate.ToString("yyyy:MM:dd")}] | [{e.Hiredate.ToString("yyyy:MM:dd")}]");
                                employees.Add(e);
                            }
                        }
                    }

                    Console.Write("Employees jünger machen? (y/n) ");
                    var eingabe = Console.ReadLine();
                    var count = 0;
                    if (eingabe == "y")
                        foreach (var e in employees)
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = $"UPDATE Employees SET Birthdate = @birthdate WHERE EmployeeId = @id";
                                command.Parameters.AddWithValue("@id", e.Id);
                                command.Parameters.AddWithValue("@birthdate", e.Birthdate.AddYears(1));

                                count += command.ExecuteNonQuery();
                            }
                        }
                    Console.WriteLine($"{count} rows affected.");

                    Console.WriteLine("\nWas hat ANTON gekauft?");
                    // Stored Procedure
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "CustOrderHist @custId";
                        command.Parameters.AddWithValue("@custId", "ANTON");

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var productName = (string)reader["ProductName"];
                                var total = (int)reader["Total"];

                                Console.WriteLine($"{productName, -30} {total,3}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
