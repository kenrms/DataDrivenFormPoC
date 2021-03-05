using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class RadioQuestionComponent : ComponentBase
    {
        [Parameter]
        public Question Question { get; set; }
        [Parameter]
        public List<OptionResponse> Responses { get; set; }
        [Parameter]
        public EventCallback RefreshQuestionList { get; set; }
        public Guid SelectedOptionId { get; set; }

        protected async override Task OnInitializedAsync() =>
            await InitializeSelectedOptionAsync();

        private async Task InitializeSelectedOptionAsync()
        {
            OptionResponse existingSelection = this.Responses
                .SingleOrDefault(optionResponse => optionResponse.IsChecked);

            this.SelectedOptionId = existingSelection != null ?
                existingSelection.Option.Id :
                this.Question.Options.First().Id;

            await RefreshQuestionListAsync();
        }

        async void SelectionChanged(ChangeEventArgs args)
        {
            this.SelectedOptionId = new Guid(args.Value.ToString());

            foreach (var optionResponse in this.Responses)
            {
                optionResponse.IsChecked =
                    optionResponse.Option.Id == this.SelectedOptionId;
            }

            await RefreshQuestionListAsync();
        }

        private async Task RefreshQuestionListAsync()
        {
            // TODO if there is a childform, wipe responses
            // TODO would need to pass in RepsonseMap as parameter

            await RefreshQuestionList.InvokeAsync();
        }
    }
}
