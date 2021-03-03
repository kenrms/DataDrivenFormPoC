using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataDrivenFormPoC.Models
{
    public class FormResponse
    {
        public Guid Id { get; set; }
        public Form Form { get; set; }
        public User FilledBy { get; set; }
        public DateTimeOffset DateSubmitted { get; set; }
        [ValidateComplexType]
        public List<OptionResponse> OptionResponses { get; set; }
    }
}