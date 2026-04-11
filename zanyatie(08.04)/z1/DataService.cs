using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace z1
{
    public class DataService
    {
        private readonly string _diaryFilePath = "diary.json";
        private readonly string _usersFilePath = "users.json";

        public async Task SaveDiaryDataAsync(ObservableCollection<StudentModel> students, ObservableCollection<GradeModel> grades)
        {
            var diaryData = new
            {
                Students = students,
                Grades = grades,
                LastUpdated = DateTime.Now
            };

            var json = JsonSerializer.Serialize(diaryData, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_diaryFilePath, json);
        }

        public async Task<(ObservableCollection<StudentModel>, ObservableCollection<GradeModel>)> LoadDiaryDataAsync()
        {
            if (!File.Exists(_diaryFilePath))
            {
                return (new ObservableCollection<StudentModel>(), new ObservableCollection<GradeModel>());
            }

            var json = await File.ReadAllTextAsync(_diaryFilePath);
            var diaryData = JsonSerializer.Deserialize<DiaryData>(json);

            if (diaryData == null)
            {
                return (new ObservableCollection<StudentModel>(), new ObservableCollection<GradeModel>());
            }

            var students = new ObservableCollection<StudentModel>();
            var grades = new ObservableCollection<GradeModel>();

            if (diaryData.Students != null)
            {
                foreach (var s in diaryData.Students)
                {
                    students.Add(s);
                }
            }

            if (diaryData.Grades != null)
            {
                foreach (var g in diaryData.Grades)
                {
                    grades.Add(g);
                }
            }

            return (students, grades);
        }

        public async Task SaveUsersAsync(ObservableCollection<UserModel> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_usersFilePath, json);
        }

        public async Task<ObservableCollection<UserModel>> LoadUsersAsync()
        {
            if (!File.Exists(_usersFilePath))
            {
                var defaultUsers = new ObservableCollection<UserModel>
                {
                    new UserModel { Id = 1, Username = "teacher", PasswordHash = GetHash("123"), Role = "Teacher", FullName = "Иван Петрович" },
                    new UserModel { Id = 2, Username = "student", PasswordHash = GetHash("123"), Role = "Student", FullName = "Иван Иванов" }
                };
                await SaveUsersAsync(defaultUsers);
                return defaultUsers;
            }

            var json = await File.ReadAllTextAsync(_usersFilePath);
            var users = JsonSerializer.Deserialize<ObservableCollection<UserModel>>(json);
            return users ?? new ObservableCollection<UserModel>();
        }

        public string GetHash(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private class DiaryData
        {
            public List<StudentModel>? Students { get; set; }
            public List<GradeModel>? Grades { get; set; }
            public DateTime LastUpdated { get; set; }
        }
    }
}