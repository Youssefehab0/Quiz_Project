using Microsoft.EntityFrameworkCore;
using Quiz_project.Data;
using Quiz_project.DTOs;
using Quiz_project.Models;

namespace Quiz_project.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuestionReadDto>> GetAllAsync()
        {
            return await _context.Questions
                .Select(q => new QuestionReadDto
                {
                    Id = q.Id,
                    Text = q.Text,
                    Type = q.Type,
                    Choices = q.Choices != null ? q.Choices.ToList() : null,
                    CorrectAnswer = q.CorrectAnswer,
                    QuizId = q.QuizId
                })
                .ToListAsync();
        }

        public async Task<QuestionReadDto?> GetByIdAsync(int id)
        {
            var q = await _context.Questions.FindAsync(id);
            if (q == null) return null;

            return new QuestionReadDto
            {
                Id = q.Id,
                Text = q.Text,
                Type = q.Type,
                Choices = q.Choices != null ? q.Choices.ToList() : null,
                CorrectAnswer = q.CorrectAnswer,
                QuizId = q.QuizId
            };
        }

        public async Task<Question> CreateAsync(QuestionCreateDto dto)
        {
            var quizExists = await _context.Quizzes.FirstOrDefaultAsync(q => q.Id == dto.QuizId);
            if (quizExists == null)
            {
                throw new ArgumentException($"Quiz with ID {dto.QuizId} does not exist.");
            }
            var question = new Question
            {
                Text = dto.Text,
                Type = dto.Type,
                Choices = dto.Choices?.ToList(), // Fix for CS0029: Convert to List<string>
                CorrectAnswer = dto.CorrectAnswer,
                QuizId = dto.QuizId,
                Quiz = quizExists
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<bool> UpdateAsync(int id, QuestionUpdateDto dto)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null) return false;

            question.Text = dto.Text;
            question.Type = dto.Type;
            question.Choices = dto.Choices?.ToList();
            question.CorrectAnswer = dto.CorrectAnswer;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null) return false;

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<QuestionReadDto?> AddToQuizAsync(int quizId, QuestionCreateDto dto)
        {
            var quiz = await _context.Quizzes.FindAsync(quizId);
            if (quiz == null) return null;

            var question = new Question
            {
                Text = dto.Text,
                Type = dto.Type,
                Choices = dto.Choices,
                CorrectAnswer = dto.CorrectAnswer,
                QuizId = quizId,
                Quiz = quiz
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return new QuestionReadDto
            {
                Id = question.Id,
                Text = question.Text,
                Type = question.Type,
                Choices = question.Choices,
                CorrectAnswer = question.CorrectAnswer,
                QuizId = question.QuizId
            };
        }

    }
}
