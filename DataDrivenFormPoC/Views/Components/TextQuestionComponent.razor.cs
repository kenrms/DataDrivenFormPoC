using DataDrivenFormPoC.Models;
using DataDrivenFormPoC.Views.Bases;
using Microsoft.AspNetCore.Components;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class TextQuestionComponent : ComponentBase
    {
        [Parameter]
        public Question Question { get; set; }

        public ElementReference ResponseTextBox { get; set; }
    }
}
