namespace Quiz_project.Models
{
    public class StudentQuiz
    {
        public int Id { get; set; }

        public required int UserId { get; set; }
        public required User User { get; set; }

        public required int QuizId { get; set; }
        public required Quiz Quiz { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SubmittedAt { get; set; }
        public double? Score { get; set; }
    }
}
