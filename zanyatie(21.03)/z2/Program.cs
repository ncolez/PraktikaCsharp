using System;

namespace z2
{
    class Student
    {
        public string Name;

        public Student(string name)
        {
            Name = name;
        }
    }

    class Department
    {
        public string Name;

        public Department(string name)
        {
            Name = name;
        }
    }

    class Professor
    {
        public string Name;

        public Professor(string name)
        {
            Name = name;
        }
    }

    class University
    {
        public Student[] Students;
        public Department Dept;
        public Professor Prof;

        public University(Student[] students, string deptName, Professor prof)
        {
            Students = students;
            Dept = new Department(deptName);
            Prof = prof;
        }

        public void ShowStudents()
        {
            Console.WriteLine("Университет, факультет: " + Dept.Name);
            Console.WriteLine("Студенты:");
            for (int i = 0; i < Students.Length; i++)
            {
                Console.WriteLine(Students[i].Name);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            University[] universities = new University[2];

            Student[] students1 = new Student[2];
            students1[0] = new Student("Иванов");
            students1[1] = new Student("Петров");
            universities[0] = new University(students1, "Физмат", new Professor("Смирнов"));

            Student[] students2 = new Student[2];
            students2[0] = new Student("Сидорова");
            students2[1] = new Student("Кузнецова");
            universities[1] = new University(students2, "ИТ", new Professor("Васильев"));

            for (int i = 0; i < universities.Length; i++)
            {
                universities[i].ShowStudents();
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}