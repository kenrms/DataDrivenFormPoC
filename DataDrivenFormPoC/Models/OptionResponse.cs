using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataDrivenFormPoC.Models
{
    public class OptionResponse
    {
        [Key, Column(Order = 0)]
        public Guid Id { get; set; }
        [Key, Column(Order = 1)]
        public Question Question { get; set; }
        [Key, Column(Order = 2)]
        public Option Option { get; set; }

        public bool IsChecked { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Too long!")]
        public string TextValue { get; set; }
        public decimal NumericValue { get; set; }
        public DateTimeOffset DateTimeValue { get; set; }
        public byte[] BlobValue { get; set; }
    }
}
