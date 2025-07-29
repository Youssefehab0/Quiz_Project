namespace Quiz_project.DTOs
{
    public class QuestionCreateDto
    {
        public string Text { get; set; } = "";
        public string Type { get; set; } = ""; // "MCQ", "TrueFalse", "FillBlank"
        public List<string>? Choices { get; set; }
        public string CorrectAnswer { get; set; } = "";
        public int QuizId { get; set; }
    }
}
