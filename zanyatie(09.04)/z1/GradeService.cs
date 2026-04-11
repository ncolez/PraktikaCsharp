using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using z1.Services;

namespace z1
{
    public class GradeService
    {
        private readonly GradeRepository _repository;

        public GradeService()
        {
            _repository = new GradeRepository();
        }

        public async Task<ObservableCollection<StudentModel>> LoadStudentsAsync()
        {
            return await _repository.GetStudentsAsync();
        }

        public async Task<ObservableCollection<GradeModel>> LoadGradesAsync()
        {
            return await _repository.GetGradesAsync();
        }

        public async Task AddGrade(ObservableCollection<GradeModel> allGrades, int studentId, string subject, int gradeValue)
        {
            var newGrade = new GradeModel
            {
                StudentId = studentId,
                Subject = subject,
                GradeValue = gradeValue,
                Date = DateTime.Now
            };

            await _repository.AddGradeAsync(newGrade);
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

        public async Task DeleteGrade(GradeModel grade, ObservableCollection<GradeModel> allGrades, ObservableCollection<GradeModel> selectedStudentGrades)
        {
            await _repository.DeleteGradeAsync(grade);
            allGrades.Remove(grade);
            selectedStudentGrades.Remove(grade);
        }

        public async Task UpdateGrade(GradeModel grade)
        {
            await _repository.UpdateGradeAsync(grade);
        }
    }
}