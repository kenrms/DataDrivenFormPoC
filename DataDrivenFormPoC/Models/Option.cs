using System;

namespace DataDrivenFormPoC.Models
{
    public class Option
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public Question Question { get; set; }
    }


}