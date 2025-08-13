namespace NeuroEase.Core.Model.Entity
{
    public class RuleCondition
    {
        public int Id { get; set; }

        public int DiagnosticRuleId { get; set; }

        public int QuestionId { get; set; }

        public string ExpectedAnswer { get; set; }

        public DiagnosticRule DiagnosticRule { get; set; }
    }
}
