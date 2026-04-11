using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace z1
{
    public class GradeService
    {
        public async Task<ObservableCollection<StudentModel>> LoadStudentsAsync()
        {
            await Task.Delay(500);
            return new ObservableCollection<StudentModel>();
        }

        public async Task<ObservableCollection<GradeModel>> LoadGradesAsync()
        {
            await Task.Delay(500);
            return new ObservableCollection<GradeModel>();
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
            if (gradesForStudent == null || gradesForStudent.Count == 0)
            {
                return 0;
            }

            double sum = 0;

            foreach (var grade in gradesForStudent)
            {
                sum += grade.GradeValue;
            }

            return Math.Round(sum / gradesForStudent.Count, 2);
        }
    }
}