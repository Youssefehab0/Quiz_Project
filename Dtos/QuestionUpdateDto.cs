namespace Quiz_project.DTOs
{
    public class QuestionUpdateDto
    {
        public string Text { get; set; } = "";
        public string Type { get; set; } = "";
        public List<string>? Choices { get; set; }
        public string CorrectAnswer { get; set; } = "";
    }
}
