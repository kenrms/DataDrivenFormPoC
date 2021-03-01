using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class DropDownQuestionComponent : ComponentBase, IOptionResponder
    {
        [Parameter]
        public Question Question { get; set; }

        [Parameter]
        public EventCallback<IOptionResponder> Callback { get; set; }

        public string SelectedOptionId { get; set; }

        void SelectionChanged(ChangeEventArgs args) =>
            this.SelectedOptionId = args.Value.ToString();

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
                    Option = option,
                };

                optionResponse.IsChecked = SelectedOptionId == option.Id.ToString();

                optionResponses.Add(optionResponse);
            }

            return optionResponses;
        }


    }
}
