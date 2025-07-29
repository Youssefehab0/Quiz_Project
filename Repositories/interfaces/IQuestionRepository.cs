using Quiz_project.DTOs;
using Quiz_project.Models;

namespace Quiz_project.Repositories
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<QuestionReadDto>> GetAllAsync();
        Task<QuestionReadDto?> GetByIdAsync(int id);
        Task<Question> CreateAsync(QuestionCreateDto dto);
        Task<bool> UpdateAsync(int id, QuestionUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<QuestionReadDto?> AddToQuizAsync(int quizId, QuestionCreateDto dto);

    }
}
