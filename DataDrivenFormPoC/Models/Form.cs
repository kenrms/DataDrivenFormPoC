using System;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Models
{
    public class Form
    {
        public Guid Id { get; set; }
        public IList<Question> Questions { get; set; }

        public Form()
        {
            this.Questions = new List<Question>();
        }
    }
}
