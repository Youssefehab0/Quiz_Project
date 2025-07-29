using Microsoft.EntityFrameworkCore;
using Quiz_project.Models;

namespace Quiz_project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<StudentQuiz> StudentQuizzes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Quiz>().ToTable("Quizzes");
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<StudentQuiz>().ToTable("StudentQuizzes");

            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.User)
                .WithMany(u => u.Quizzes)
                .HasForeignKey(q => q.UserId);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(qz => qz.Questions)
                .HasForeignKey(q => q.QuizId);

            modelBuilder.Entity<StudentQuiz>()
                .HasOne(sq => sq.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(sq => sq.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentQuiz>()
                .HasOne(sq => sq.Quiz)
                .WithMany(q => q.StudentQuizzes)
                .HasForeignKey(sq => sq.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
