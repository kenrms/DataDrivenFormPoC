using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class DateQuestionComponent : ComponentBase, IOptionResponder
    {
        [Parameter]
        public Question Question { get; set; }
        [Parameter]
        public EventCallback<IOptionResponder> Callback { get; set; }
        [Parameter]
        public List<OptionResponse> Responses { get; set; }

        public DateTimeOffset DateInput { get; private set; }

        protected Guid GetOptionId() => Question.Options.First().Id;

        protected async override Task OnInitializedAsync()
        {
            await this.Callback.InvokeAsync(this);
        }

        public IList<OptionResponse> GetOptionResponses()
        {
            var optionResponses = new List<OptionResponse>();

            var optionResponse = new OptionResponse
            {
                Question = Question,
                Option = Question.Options.First(),
            };

            optionResponse.DateTimeValue = DateInput;

            optionResponses.Add(optionResponse);

            return optionResponses;
        }
    }
}
