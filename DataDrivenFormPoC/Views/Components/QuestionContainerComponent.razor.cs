using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class QuestionContainerComponent : ComponentBase
    {
        [Parameter]
        public Question Question { get; set; }
        [Parameter]
        public int QuestionNumber { get; set; }
        [Parameter]
        public bool ShowNumberAsLetter { get; set; }
        [Parameter]
        public List<OptionResponse> Responses { get; set; }
        [Parameter]
        public List<string> ValidationMessages { get; set; }
        [Parameter]
        public Dictionary<Guid, List<OptionResponse>> ResponsesMap { get; set; }
        [Parameter]
        public Dictionary<Guid, List<string>> ValidationMessagesMap { get; set; }
        [Parameter]
        public EventCallback RefreshQuestionList { get; set; }
    }
}
