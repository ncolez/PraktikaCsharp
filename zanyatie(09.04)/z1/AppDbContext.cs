using Microsoft.EntityFrameworkCore;

namespace z1.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<GradeModel> Grades { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=diary.db");
        }
    }
}