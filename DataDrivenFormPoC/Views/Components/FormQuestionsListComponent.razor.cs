using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class FormQuestionsListComponent : ComponentBase
    {
        [Parameter]
        public List<Question> Questions { get; set; }
        [Parameter]
        public Dictionary<Guid, List<OptionResponse>> ResponsesMap { get; set; }
        [Parameter]
        public Dictionary<Guid, List<string>> ValidationMessagesMap { get; set; }
        [Parameter]
        public bool IsHidden { get; set; }

        public void RefreshQuestionList()
        {
            this.StateHasChanged();
        }
    }
}
