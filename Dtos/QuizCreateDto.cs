namespace Quiz_project.Dtos
{
    public class QuizCreateDto
    {
        public required string Title { get; set; } 
        public string? Description { get; set; }
        public DateTime EndAt { get; set; }
        public int UserId { get; set; } // Assuming UserId is an integer representing the user's ID
    }
}
