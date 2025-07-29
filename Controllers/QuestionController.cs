using Microsoft.AspNetCore.Mvc;
using Quiz_project.DTOs;
using Quiz_project.Repositories;

namespace Quiz_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepo;

        public QuestionController(IQuestionRepository questionRepo)
        {
            _questionRepo = questionRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var questions = await _questionRepo.GetAllAsync();
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var question = await _questionRepo.GetByIdAsync(id);
            if (question == null)
                return NotFound();

            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionCreateDto dto)
        {
            var created = await _questionRepo.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] QuestionUpdateDto dto)
        {
            var updated = await _questionRepo.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _questionRepo.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpPost("quiz/{quizId}")]
        public async Task<IActionResult> AddToQuiz(int quizId, [FromBody] QuestionCreateDto dto)
        {
            var created = await _questionRepo.AddToQuizAsync(quizId, dto);
            if (created == null) return NotFound("Quiz not found");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}
