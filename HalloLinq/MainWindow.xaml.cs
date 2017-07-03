using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace HalloLinq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ConnectionString = "Data Source=.;Initial Catalog=NORTHWND;Integrated Security=True";

        public MainWindow()
        {
            InitializeComponent();
        }

        private IEnumerable<Order> GetOrders()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id=OrderID, EmployeeId, City = ShipCity FROM Orders";

                    using (var reader = command.ExecuteReader())
                        while (reader.Read())
                            yield return new Order
                            {
                                Id = (int)reader["Id"],
                                EmployeeId = (int)reader["EmployeeId"],
                                City = (string)reader["City"]
                            };
                }
            }
        }
        private IEnumerable<Employee> GetEmployees()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command =  connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id=EmployeeId, Name=(Firstname + ' ' + Lastname), Expreience = (Year(Hiredate) - Year(Birthdate)) FROM Employees";

                    using (var reader = command.ExecuteReader())
                        while (reader.Read())
                            yield return new Employee
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Experience = (int)reader["Expreience"]
                            };
                }
            }
        }

        private void AllEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            myDataGrid.ItemsSource = GetEmployees();
        }

        private void RestrictionButton_Click(object sender, RoutedEventArgs eargs)
        {
            // Linq
            //var query = from e in GetEmployees()
            //            where e.Experience < 40
            //            select e;

            // Fluid API
            var query = GetEmployees().Where(e => e.Experience < 40);

            myDataGrid.ItemsSource = query;
        }

        private void OrderingButton_Click(object sender, RoutedEventArgs eargs)
        {
            //var query = from e in GetEmployees()
            //            orderby e.Name descending, e.Experience ascending
            //            select e;

            var query = GetEmployees().OrderByDescending(e => e.Name)
                                      .ThenBy(e => e.Experience);

            myDataGrid.ItemsSource = query;
        }

        private void ProjectionButton_Click(object sender, RoutedEventArgs eargs)
        {
            //var query = from e in GetEmployees()
            //            select new { e.Name, WasKannEr = e.Experience };

            var query = GetEmployees().Select(e => new { e.Name, Erfahrung = e.Experience });

            myDataGrid.ItemsSource = query;
        }

        private void GroupingButton_Click(object sender, RoutedEventArgs eargs)
        {
            //var query = from e in GetEmployees()
            //            group e by e.Name[0] into g
            //            select new { AnfangsBuchstabe = g.Key, Employees = g };

            var query = GetEmployees().GroupBy(e => e.Name[0]).Select(g => new { AnfangsBuchstabe = g.Key, Employees = g });

            myDataGrid.ItemsSource = query;
        }

        private void PartitioningButton_Click(object sender, RoutedEventArgs eargs)
        {
            var query = GetEmployees().Skip(3).Take(3);

            myDataGrid.ItemsSource = query;
        }

        private void ElementOperatorsButton_Click(object sender, RoutedEventArgs eargs)
        {
            var employees = GetEmployees();

            //var employee = employees.First();
            //var employee = employees.FirstOrDefault();

            //var employee = employees.First(e => e.Name.StartsWith("A"));
            //var employee = employees.FirstOrDefault(e => e.Name.StartsWith("A"));

            //var employee = employees.Single();
            //var employee = employees.SingleOrDefault();

            //var employee = employees.Single(e => e.Name.Length > 5);
            //var employee = employees.SingleOrDefault(e => e.Name.Length > 5);

            // Wird von Sql Server nicht unterstützt!!
            //var employee = employees.Last();
            var employee = employees.LastOrDefault();
            //var employee = employees.Last(e => e.Id == 1);
            //var employee = employees.LastOrDefault(e => e.Id == 1);

            elementOperatorsTextBlock.Text = employee.Name;
        }

        private void QuanitfyingButton_Click(object sender, RoutedEventArgs eargs)
        {
            var allmoreThan40YearsExperience = GetEmployees().All(e => e.Experience > 40);
            var anymoreThan40YearsExperience = GetEmployees().Any(e => e.Experience > 40);

            QuantifyingAllTextBlock.Text = $"{(allmoreThan40YearsExperience ? "Alle" : "Nicht alle")} Employees haben mehr als 40 Jahre Erfahrung.";
            QuantifyingAnyTextBlock.Text = $"{(anymoreThan40YearsExperience ? "Mindestens einer" : "Keiner")} der Employees hat mehr als 40 Jahre Erfahrung.";
        }

        private void AggregatingButton_Click(object sender, RoutedEventArgs eargs)
        {
            var employees = GetEmployees();
            CountTextBlock.Text = $"{employees.Count()} Employees in DB";

            MinTextBlock.Text = $"Die geringste Erfahrung liegt bei {employees.Min(e => e.Experience)}";
            MaxTextBlock.Text = $"Die meiste Erfahrung liegt bei {employees.Max(e => e.Experience)}";
            AverageTextBlock.Text = $"Die durchschnittliche Erfahrung liegt bei {employees.Average(e => e.Experience)}";
            SumTextBlock.Text = $"Die gesammte Erfahrung liegt bei {employees.Sum(e => e.Experience)}";
        }

        private void InnerJoinButton_Click(object sender, RoutedEventArgs eargs)
        {
            var employees = GetEmployees();
            var orders = GetOrders();

            //var query = from e in employees
            //            join o in orders on e.Id equals o.EmployeeId
            //            select new { e.Name, o.City };

            var query = employees.Join(
                inner: orders,
                outerKeySelector: e => e.Id,
                innerKeySelector: o => o.EmployeeId,
                resultSelector: (e, o) => new { e.Name, o.City });

            myDataGrid.ItemsSource = query;
        }

        private void GroupJoinButton_Click(object sender, RoutedEventArgs eargs)
        {
            var employees = GetEmployees();
            var orders = GetOrders();

            //var query = from e in employees
            //            join o in orders on e.Id equals o.EmployeeId into g
            //            select new { e.Name, Count = g.Count() };

            var query = employees.GroupJoin(
                inner: orders,
                outerKeySelector: e => e.Id,
                innerKeySelector: o => o.EmployeeId,
                resultSelector: (e, o) => new { e.Name, Count = o.Count() });

            myDataGrid.ItemsSource = query;

        }

        private void CrossJoinButton_Click(object sender, RoutedEventArgs eargs)
        {
            var employees = GetEmployees();
            var orders = GetOrders();

            //var whatever = from e in employees
            //               from o in orders
            //               select new { e.Name, o.City };

            var whatever = employees.Join(
                inner: orders,
                outerKeySelector: _ => new { },
                innerKeySelector: _ => new { },
                resultSelector: (e, o) => new { e.Name, o.City });

            myDataGrid.ItemsSource = whatever;
        }
    }

    internal class Mitarbeiter
    {
        public string Name { get; set; }
        public int Erfahrung { get; set; }
    }
}