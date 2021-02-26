using System;
using DataDrivenFormPoC.Models;

namespace DataDrivenFormPoC.Views.Components
{
    public abstract class QuestionBase
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public bool IsRequired { get; set; }
        public ResponseType ResponseType { get; set; }
        public abstract Type GetViewComponent();
    }
}