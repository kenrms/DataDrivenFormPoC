using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class RadioQuestionComponent : ComponentBase, IOptionResponder
    {
        [Parameter]
        public Question Question { get; set; }

        [Parameter]
        public EventCallback<IOptionResponder> Callback { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await this.Callback.InvokeAsync(this);
        }

        public IList<OptionResponse> GetOptionResponses()
        {
            var optionResponses = new List<OptionResponse>();

            foreach (var option in this.Question.Options)
            {
                var optionResponse = new OptionResponse
                {
                    Question = Question,
                    Option = Question.Options.First(),
                };

                // TODO set response

                optionResponses.Add(optionResponse);
            }

            return optionResponses;
        }


    }
}
