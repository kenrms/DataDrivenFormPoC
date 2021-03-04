using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataDrivenFormPoC.Models
{
    public class OptionChildForm
    {
        public Guid Id { get; set; }
        [ForeignKey("Option")]
        public Option ParentOption { get; set; }
        public IList<Question> Questions { get; set; }

        public OptionChildForm()
        {
            Questions = new List<Question>();
        }
    }
}
