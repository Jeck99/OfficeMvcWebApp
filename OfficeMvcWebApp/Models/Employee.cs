using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeMvcWebApp.Models
{
    public class Employee
    {
        string firstName;
        string lastName;
        int age;
        DateTime birthday;
        string email;

        public Employee(string firstName, string lastName, int age, DateTime birthday, string email)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Birthday = birthday;
            this.Email = email;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Age { get => age; set => age = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public string Email { get => email; set => email = value; }
    }
}