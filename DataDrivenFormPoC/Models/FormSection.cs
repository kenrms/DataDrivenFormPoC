using System;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Models
{
    public class FormSection
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public Form Form { get; set; }
        public IList<Question> Questions { get; set; }

        public FormSection()
        {
            this.Questions = new List<Question>();
        }
    }
}
