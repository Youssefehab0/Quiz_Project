using Microsoft.AspNetCore.Mvc;
using Quiz_project.Dtos;
using Quiz_project.DTOs;
using Quiz_project.Repositories;

namespace Quiz_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepository _quizRepository;

        public QuizController(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizReadDto>>> GetAll()
        {
            var quizzes = await _quizRepository.GetAllAsync();
            return Ok(quizzes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizReadDto>> GetById(int id)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null) return NotFound();
            return Ok(quiz);
        }

        [HttpPost]
        public async Task<ActionResult<QuizReadDto>> Create(QuizCreateDto dto)
        {
            var createdQuiz = await _quizRepository.CreateAsync(dto);
            if (createdQuiz == null) return BadRequest("Invalid quiz data");

            return CreatedAtAction(nameof(GetById), new { id = createdQuiz.Id }, createdQuiz);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, QuizUpdateDto dto)
        {
            var updated = await _quizRepository.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _quizRepository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
