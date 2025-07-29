namespace Quiz_project.Dtos
{
    public class QuizUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime EndAt { get; set; }
    }
}