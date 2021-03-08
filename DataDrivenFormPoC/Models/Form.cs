using System;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Models
{
    public class Form
    {
        public Guid Id { get; set; }
        public IList<FormSection> Sections { get; set; }

        public Form()
        {
            this.Sections = new List<FormSection>();
        }
    }
}
