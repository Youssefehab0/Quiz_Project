namespace Quiz_project.DTOs
{
    public class QuestionReadDto
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public required string Type { get; set; }
        public List<string>? Choices { get; set; }
        public required string CorrectAnswer { get; set; }
        public int QuizId { get; set; }
        }
}
