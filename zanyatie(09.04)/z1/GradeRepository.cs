using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using z1.Data;

namespace z1.Services
{
    public class GradeRepository
    {
        private readonly AppDbContext _context;

        public GradeRepository()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
        }

        public async Task<ObservableCollection<StudentModel>> GetStudentsAsync()
        {
            var students = await _context.Students.ToListAsync();
            return new ObservableCollection<StudentModel>(students);
        }

        public async Task<ObservableCollection<GradeModel>> GetGradesAsync()
        {
            var grades = await _context.Grades.ToListAsync();
            return new ObservableCollection<GradeModel>(grades);
        }

        public async Task<ObservableCollection<UserModel>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return new ObservableCollection<UserModel>(users);
        }

        public async Task AddGradeAsync(GradeModel grade)
        {
            await _context.Grades.AddAsync(grade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGradeAsync(GradeModel grade)
        {
            _context.Grades.Update(grade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGradeAsync(GradeModel grade)
        {
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
        }

        public async Task AddStudentAsync(StudentModel student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserAsync(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}