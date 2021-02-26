using System;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Domain
{
    public class Question
    {
        public Guid Id { get; set; }
        public ResponseType ResponseType { get; set; }
        public string QuestionText { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }
        public Form Form { get; set; }
        public int FormId { get; set; }
        public IList<Option> Options { get; set; }

        public Question()
        {
            this.Options = new List<Option>();
        }
    }
}
