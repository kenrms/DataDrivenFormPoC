using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace DataDrivenFormPoC.Models
{
    public class OptionResponse
    {
        [Key, Column(Order = 0)]
        public Guid Id { get; set; }
        [Key, Column(Order = 1)]
        public Question Question { get; set; }
        [Key, Column(Order = 2)]
        public User FilledBy { get; set; }
        public Option Option { get; set; }

        public bool IsChecked { get; set; }
        public string TextValue { get; set; }
        public decimal NumericValue { get; set; }
        public DateTimeOffset DateTimeValue { get; set; }
        public byte[] BlobValue { get; set; }
    }
}
