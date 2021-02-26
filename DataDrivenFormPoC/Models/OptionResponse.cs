using System;

namespace DataDrivenFormPoC.Models
{
    public class OptionResponse
    {
        public Guid Id { get; set; }
        public FormResponse FormResponse { get; set; }
        public int FormResponseId { get; set; }
        public User FilledBy { get; set; }
        public int FilledById { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
        public Option Option { get; set; }
        public int OptionId { get; set; }

        public bool IsChecked { get; set; }
        public string TextValue { get; set; }
        public decimal NumericValue { get; set; }
        public DateTimeOffset DateTimeValue { get; set; }
        public byte[] BlobValue { get; set; }
    }
}
