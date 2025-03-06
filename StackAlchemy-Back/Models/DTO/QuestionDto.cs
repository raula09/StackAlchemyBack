namespace StackAlchemy_Back.Models.DTO
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public List<AnswerDto> Answers { get; set; } 
    }
}
