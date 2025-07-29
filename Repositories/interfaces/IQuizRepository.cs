using Quiz_project.Dtos;
using Quiz_project.DTOs;

namespace Quiz_project.Repositories
{
    public interface IQuizRepository
    {
        Task<IEnumerable<QuizReadDto>> GetAllAsync();
        Task<QuizReadDto?> GetByIdAsync(int id);
        Task<QuizReadDto?> CreateAsync(QuizCreateDto dto);
        Task<bool> UpdateAsync(int id, QuizUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
