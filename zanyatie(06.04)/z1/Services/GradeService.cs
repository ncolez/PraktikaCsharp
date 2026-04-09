using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace z1
{
    public class GradeService
    {
        public async Task<ObservableCollection<StudentModel>> LoadStudentsAsync()
        {
            await Task.Delay(2000);

            var students = new ObservableCollection<StudentModel>();

            students.Add(new StudentModel { Id = 1, FullName = "Иван Иванов" });
            students.Add(new StudentModel { Id = 2, FullName = "Петр Петров" });
            students.Add(new StudentModel { Id = 3, FullName = "Анна Сидорова" });

            return students;
        }

        public async Task<ObservableCollection<GradeModel>> LoadGradesAsync()
        {
            await Task.Delay(1500);

            var grades = new ObservableCollection<GradeModel>();

            grades.Add(new GradeModel { Id = 1, StudentId = 1, Subject = "Математика", GradeValue = 8, Date = DateTime.Now.AddDays(-10) });
            grades.Add(new GradeModel { Id = 2, StudentId = 1, Subject = "Физика", GradeValue = 7, Date = DateTime.Now.AddDays(-8) });
            grades.Add(new GradeModel { Id = 3, StudentId = 1, Subject = "Программирование", GradeValue = 9, Date = DateTime.Now.AddDays(-5) });
            grades.Add(new GradeModel { Id = 4, StudentId = 1, Subject = "Английский", GradeValue = 6, Date = DateTime.Now.AddDays(-3) });

            grades.Add(new GradeModel { Id = 5, StudentId = 2, Subject = "Математика", GradeValue = 4, Date = DateTime.Now.AddDays(-7) });
            grades.Add(new GradeModel { Id = 6, StudentId = 2, Subject = "Физика", GradeValue = 5, Date = DateTime.Now.AddDays(-4) });
            grades.Add(new GradeModel { Id = 7, StudentId = 2, Subject = "Русский язык", GradeValue = 7, Date = DateTime.Now.AddDays(-2) });

            grades.Add(new GradeModel { Id = 8, StudentId = 3, Subject = "Математика", GradeValue = 10, Date = DateTime.Now.AddDays(-6) });
            grades.Add(new GradeModel { Id = 9, StudentId = 3, Subject = "Программирование", GradeValue = 9, Date = DateTime.Now.AddDays(-2) });
            grades.Add(new GradeModel { Id = 10, StudentId = 3, Subject = "Английский", GradeValue = 8, Date = DateTime.Now.AddDays(-1) });
            grades.Add(new GradeModel { Id = 11, StudentId = 3, Subject = "Физика", GradeValue = 7, Date = DateTime.Now.AddDays(-4) });

            return grades;
        }

        public void AddGrade(ObservableCollection<GradeModel> allGrades, int studentId, string subject, int gradeValue)
        {
            var newGrade = new GradeModel
            {
                Id = allGrades.Count + 1,
                StudentId = studentId,
                Subject = subject,
                GradeValue = gradeValue,
                Date = DateTime.Now
            };

            allGrades.Add(newGrade);
        }

        public double GetAverageGrade(ObservableCollection<GradeModel> gradesForStudent)
        {
            if (gradesForStudent == null || gradesForStudent.Count == 0) return 0;

            double sum = 0;

            foreach (var grade in gradesForStudent)
            {
                sum += grade.GradeValue;
            }

            return Math.Round(sum / gradesForStudent.Count, 2);
        }
    }
}