namespace StackAlchemy_Back.Models.DTO
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Code { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
    }
}
