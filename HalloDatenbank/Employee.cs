using System;

namespace HalloDatenbank
{
    public class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Hiredate { get; set; }
    }
}
