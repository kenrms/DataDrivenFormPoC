using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

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

        protected override void OnInitialized() =>
            InitializeSelectedOption();

        private void InitializeSelectedOption()
        {
            OptionResponse existingSelection = this.Responses
                .SingleOrDefault(optionResponse => optionResponse.IsChecked);

            this.SelectedOptionId = existingSelection != null ?
                existingSelection.Option.Id :
                this.Question.Options.First().Id;
        }

        async void SelectionChanged(ChangeEventArgs args)
        {
            this.SelectedOptionId = new Guid(args.Value.ToString());

            foreach (var optionResponse in this.Responses)
            {
                optionResponse.IsChecked =
                    optionResponse.Option.Id == this.SelectedOptionId;
            }

            if (Question.Options.Any(option => option.ChildForm != null))
            {
                await RefreshQuestionList.InvokeAsync();
            }
        }
    }
}
