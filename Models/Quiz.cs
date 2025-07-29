namespace Quiz_project.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public required string Title { get; set; } 
        public  string? Description { get; set; } 
        public required DateTime CreatedAt { get; set; } 
        public required DateTime EndAt { get; set; } 
        public required int UserId { get; set; }
        public required User User { get; set; }
        public List<Question>? Questions { get; set; }
        public List<StudentQuiz>? StudentQuizzes { get; set; }
    }
}
