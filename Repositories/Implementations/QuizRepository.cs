using Microsoft.EntityFrameworkCore;
using Quiz_project.Data;
using Quiz_project.Dtos;
using Quiz_project.DTOs;
using Quiz_project.Models;

namespace Quiz_project.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;

        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<QuizReadDto?> CreateAsync(QuizCreateDto dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null) return null;

            var quiz = new Quiz
            {
                Title = dto.Title,
                Description = dto.Description,
                CreatedAt = DateTime.Now,
                EndAt = dto.EndAt,
                UserId = dto.UserId,
                User = user
            };

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            return new QuizReadDto
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                CreatedAt = quiz.CreatedAt,
                EndAt = quiz.EndAt,
                UserName = user.Name
            };
        }

        public async Task<IEnumerable<QuizReadDto>> GetAllAsync()
        {
            var quizzes = await _context.Quizzes.Include(q => q.User).ToListAsync();

            return quizzes.Select(q => new QuizReadDto
            {
                Id = q.Id,
                Title = q.Title,
                Description = q.Description,
                CreatedAt = q.CreatedAt,
                EndAt = q.EndAt,
                UserName = q.User?.Name ?? ""
            });
        }

        public async Task<QuizReadDto?> GetByIdAsync(int id)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.User)
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null) return null;

            return new QuizReadDto
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                CreatedAt = quiz.CreatedAt,
                EndAt = quiz.EndAt,
                UserName = quiz.User?.Name ?? "",
                Questions = quiz.Questions?.Select(q => new QuestionReadDto
                {
                    Id = q.Id,
                    Text = q.Text,
                    Type = q.Type,
                    Choices = q.Choices,
                    CorrectAnswer = q.CorrectAnswer
                }).ToList()
            };
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null) return false;

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, QuizUpdateDto dto)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null) return false;

            quiz.Title = dto.Title;
            quiz.Description = dto.Description;
            quiz.EndAt = dto.EndAt;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
