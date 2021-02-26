using System;

namespace DataDrivenFormPoC.Models
{
    public class FormResponse
    {
        public Guid Id { get; set; }
        public Form Form { get; set; }
        public int FormId { get; set; }
        public DateTimeOffset DateSubmitted { get; set; }
    }
}