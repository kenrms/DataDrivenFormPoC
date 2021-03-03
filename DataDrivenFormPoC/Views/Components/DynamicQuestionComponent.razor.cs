using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class DynamicQuestionComponent : ComponentBase
    {
        [Parameter]
        public Question Question { get; set; }
        [Parameter]
        public List<OptionResponse> Responses { get; set; }
        [Parameter]
        public List<ValidationRule> ValidationRules { get; set; }

        [Parameter]
        public RenderFragment TextQuestionFragment { get; set; }
        [Parameter]
        public RenderFragment TextAreaQuestionFragment { get; set; }
        [Parameter]
        public RenderFragment DropDownQuestionFragment { get; set; }
        [Parameter]
        public RenderFragment RadioQuestionFragment { get; set; }
        [Parameter]
        public RenderFragment CheckBoxQuestionFragment { get; set; }
        [Parameter]
        public RenderFragment DateQuestionFragment { get; set; }
        [Parameter]
        public RenderFragment DateTimeQuestionFragment { get; set; }
        [Parameter]
        public RenderFragment UnknownQuestionFragment { get; set; }

        private RenderFragment GetQuestionFragmentToRender()
        {
            return this.Question.ResponseType switch
            {
                ResponseType.RawText => TextQuestionFragment,
                ResponseType.SingleChoiceDropDown => DropDownQuestionFragment,
                ResponseType.SingleChoiceRadio => RadioQuestionFragment,
                ResponseType.MultipleChoice => CheckBoxQuestionFragment,
                ResponseType.Date => DateQuestionFragment,
                _ => UnknownQuestionFragment,
            };
        }
    }
}
