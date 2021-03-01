using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class CheckBoxQuestionComponent : ComponentBase, IOptionResponder
    {
        [Parameter]
        public Question Question { get; set; }

        [Parameter]
        public EventCallback<IOptionResponder> Callback { get; set; }

        public Dictionary<Option, bool> CheckedMap { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await this.Callback.InvokeAsync(this);
            InitializeCheckedMap();
        }

        private void InitializeCheckedMap()
        {
            this.CheckedMap = new Dictionary<Option, bool>();
            foreach (var option in this.Question.Options)
            {
                this.CheckedMap[option] = false;
            }
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

                optionResponse.IsChecked = this.CheckedMap[option];

                optionResponses.Add(optionResponse);
            }

            return optionResponses;
        }


    }
}
