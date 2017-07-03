using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace DbIndependent
{
    class Program
    {
        static void Main(string[] args)
        {
            var bedingung = 5 > 8; 

            string connectionString;
            DbProviderFactory factory;

            if(bedingung)
            {
                connectionString = "my fancy AccesConnectionstring";
                factory = System.Data.Odbc.OdbcFactory.Instance;
            }
            else
            {
                connectionString = "Data Source=.;Initial Catalog=NORTHWND;Integrated Security=True";
                factory = SqlClientFactory.Instance;
            }


            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Count(*) FROM Employees WHERE Firstname LIKE @name+'%'";

                    //var p = command.CreateParameter();
                    //p.ParameterName = "@name";
                    //p.Value = "M";
                    //command.Parameters.Add(p);

                    command.AddParameterWithValue("@name", "M");

                    var result = command.ExecuteScalar();
                    Console.WriteLine($"{result} Employees in Db.");
                }
            }

            Console.ReadKey();
        }
    }
}
