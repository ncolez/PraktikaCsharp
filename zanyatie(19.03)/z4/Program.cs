using System;

namespace z4
{
    public partial class Student
    {
        public string Name;
        public string Group;
        public double GPA;

        public Student(string name, string group, double gpa)
        {
            Name = name;
            Group = group;
            GPA = gpa;
        }
    }

    public partial class Student
    {
        public static Student[] GetTopStudents(Student[] students)
        {
            int count = 0;
            for (int i = 0; i < students.Length; i++)
            {
                if (students[i].GPA > 4.5)
                {
                    count++;
                }
            }

            Student[] result = new Student[count];
            int index = 0;
            for (int i = 0; i < students.Length; i++)
            {
                if (students[i].GPA > 4.5)
                {
                    result[index] = students[i];
                    index++;
                }
            }
            return result;
        }

        public static Student[] GetStudentsByGroup(Student[] students, string group)
        {
            int count = 0;
            for (int i = 0; i < students.Length; i++)
            {
                if (students[i].Group == group)
                {
                    count++;
                }
            }

            Student[] result = new Student[count];
            int index = 0;
            for (int i = 0; i < students.Length; i++)
            {
                if (students[i].Group == group)
                {
                    result[index] = students[i];
                    index++;
                }
            }
            return result;
        }
    }

    class University
    {
        public Student[] students;

        public University(Student[] students)
        {
            this.students = students;
        }

        public Student[] GetTopStudents()
        {
            return Student.GetTopStudents(students);
        }

        public Student[] GetStudentsByGroup(string group)
        {
            return Student.GetStudentsByGroup(students, group);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Student[] students = new Student[5];
            students[0] = new Student("Иванов", "ПИ-31", 4.8);
            students[1] = new Student("Петрова", "ПИ-31", 4.2);
            students[2] = new Student("Сидоров", "ПИ-32", 4.9);
            students[3] = new Student("Кузнецова", "ПИ-32", 4.0);
            students[4] = new Student("Смирнов", "ПИ-31", 4.7);

            University university = new University(students);

            Console.WriteLine("Студенты с GPA > 4.5:");
            Student[] top = university.GetTopStudents();
            for (int i = 0; i < top.Length; i++)
            {
                Console.WriteLine(top[i].Name + " - " + top[i].Group + " - " + top[i].GPA);
            }

            Console.WriteLine("\nСтуденты группы ПИ-31:");
            Student[] group = university.GetStudentsByGroup("ПИ-31");
            for (int i = 0; i < group.Length; i++)
            {
                Console.WriteLine(group[i].Name + " - " + group[i].GPA);
            }

            Console.ReadLine();
        }
    }
}