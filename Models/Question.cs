using Quiz_project.Models;

public class Question
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public required string Type { get; set; } // "MCQ", "TrueFalse", "FillBlank"

    public List<string>? Choices { get; set; }

    public required string CorrectAnswer { get; set; }

    public required int QuizId { get; set; }
    public required Quiz Quiz { get; set; }
}
