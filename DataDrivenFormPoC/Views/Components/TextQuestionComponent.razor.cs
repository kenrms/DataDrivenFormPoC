using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class TextQuestionComponent : ComponentBase, IOptionResponder, IHandleProdividedOptionResponses
    {
        [Parameter]
        public Question Question { get; set; }

        [Parameter]
        public EventCallback<IOptionResponder> Callback { get; set; }

        [Parameter]
        public List<OptionResponse> Responses { get; set; }

        public string TextInput { get; private set; }

        protected Guid GetOptionId() => Question.Options.First().Id;

        protected async override Task OnInitializedAsync()
        {
            await this.Callback.InvokeAsync(this);
            HandleProvidedOptionResponses();
        }

        public void HandleProvidedOptionResponses()
        {
            if (this.Responses.Any())
            {
                this.TextInput = this.Responses.Single().TextValue;
            }
        }

        public IList<OptionResponse> GetOptionResponses()
        {
            OptionResponse optionResponse;

            if (this.Responses.Any())
            {
                optionResponse = this.Responses.Single();
            }
            else
            {
                optionResponse = new OptionResponse();
                this.Responses.Add(optionResponse);
            }

            optionResponse.Question = Question;
            optionResponse.Option = Question.Options.First();
            optionResponse.TextValue = TextInput;

            return this.Responses;
        }
    }
}
