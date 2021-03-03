using System;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public ResponseType ResponseType { get; set; }
        public string QuestionText { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }
        public Form Form { get; set; }
        public IList<Option> Options { get; set; }
        public IList<QuestionValidationRule> QuestionValidationRules { get; set; }

        public Question()
        {
            this.Options = new List<Option>();
            this.QuestionValidationRules = new List<QuestionValidationRule>();
        }
    }
}
