namespace DataDrivenFormPoC.Domain
{
    public class Option
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}