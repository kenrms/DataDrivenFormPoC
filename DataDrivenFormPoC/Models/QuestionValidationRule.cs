using System;

namespace DataDrivenFormPoC.Models
{
    public class QuestionValidationRule
    {
        public Guid Id { get; set; }
        public Question Question { get; set; }
        public ValidationRule ValidationRule { get; set; }
        public int Order { get; set; }
        public string ValidationErrorMessage { get; internal set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public string Pattern { get; set; }
    }
}