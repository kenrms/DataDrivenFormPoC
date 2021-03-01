using System;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Models
{
    public class FormResponse
    {
        public Guid Id { get; set; }
        public Form Form { get; set; }
        public User FilledBy { get; set; }
        public DateTimeOffset DateSubmitted { get; set; }
        public List<OptionResponse> OptionResponses { get; set; }
    }
}