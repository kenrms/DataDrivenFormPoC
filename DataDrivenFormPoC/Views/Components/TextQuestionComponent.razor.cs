using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class TextQuestionComponent : ComponentBase
    {
        [Parameter]
        public Question Question { get; set; }

        [Parameter]
        public List<OptionResponse> Responses { get; set; }

        protected Guid GetOptionId() =>
            Question.Options.First().Id;

        public OptionResponse GetOptionResponse() =>
            this.Responses.Single();
    }
}
