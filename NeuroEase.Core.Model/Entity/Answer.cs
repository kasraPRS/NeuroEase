using Core.Model.Layer.Entity;

namespace NeuroEase.Core.Data
{
    public class Answer
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public bool Response { get; set; }
        public string SessionId { get; set; }
        public Question Question { get; set; }
        public string Text { get; set; }
    }
}