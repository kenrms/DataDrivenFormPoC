using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class TextQuestionComponent : ComponentBase, IOptionResponder
    {
        [Parameter]
        public Question Question { get; set; }

        [Parameter]
        public FormComponent FormComponentRef { get; set; }

        public string TextInput { get; private set; }

        protected Guid GetOptionId() => Question.Options.First().Id;

        protected override void OnInitialized()
        {
            this.FormComponentRef.OptionResponders.Add(this);
        }

        public OptionResponse GetOptionResponse()
        {
            var optionResponse = new OptionResponse
            {
                Question = Question,
                Option = Question.Options.First(),
            };

            optionResponse.TextValue = TextInput;

            return optionResponse;
        }
    }
}
