namespace NeuroEase.Core.Model.Entity
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Answers { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
