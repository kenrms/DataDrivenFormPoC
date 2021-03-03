using System;

namespace DataDrivenFormPoC.Models
{
    public class QuestionValidationRule
    {
        public Guid Id { get; set; }
        public Question Question { get; set; }
        public ValidationRule ValidationRule { get; set; }
    }
}