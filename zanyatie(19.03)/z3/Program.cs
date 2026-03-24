using System;

namespace z3
{
    abstract class Employee
    {
        public string Name;
        public int Age;
        public double Salary;

        public Employee(string name, int age, double salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }
    }

    sealed class Manager : Employee
    {
        public Manager(string name, int age, double salary) : base(name, age, salary) { }
    }

    sealed class Developer : Employee
    {
        public Developer(string name, int age, double salary) : base(name, age, salary) { }
    }

    class Company
    {
        public Employee[] employees;

        public Company(Employee[] employees)
        {
            this.employees = employees;
        }

        public Employee GetHighestPaidEmployee()
        {
            Employee highest = employees[0];
            for (int i = 1; i < employees.Length; i++)
            {
                if (employees[i].Salary > highest.Salary)
                {
                    highest = employees[i];
                }
            }
            return highest;
        }

        public double GetAverageAge()
        {
            int sum = 0;
            for (int i = 0; i < employees.Length; i++)
            {
                sum = sum + employees[i].Age;
            }
            return (double)sum / employees.Length;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee[] employees = new Employee[4];
            employees[0] = new Manager("Иван", 45, 120000);
            employees[1] = new Developer("Анна", 28, 95000);
            employees[2] = new Manager("Петр", 52, 150000);
            employees[3] = new Developer("Елена", 24, 80000);

            Company company = new Company(employees);

            Employee highest = company.GetHighestPaidEmployee();
            Console.WriteLine("Самый высокооплачиваемый: " + highest.Name + " - " + highest.Salary);

            double avgAge = company.GetAverageAge();
            Console.WriteLine("Средний возраст: " + avgAge);

            Console.ReadLine();
        }
    }
}