using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class QuestionComponent : ComponentBase
    {
        [Parameter]
        public int QuestionNumber { get; set; }
        [Parameter]
        public Question Question { get; set; }
        [Parameter]
        public List<OptionResponse> Responses { get; set; }
    }
}
