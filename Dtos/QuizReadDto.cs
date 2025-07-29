namespace Quiz_project.DTOs
{
    public class QuizReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndAt { get; set; }
        public string UserName { get; set; } = "";
        public List<QuestionReadDto>? Questions { get; set; }
    }
}