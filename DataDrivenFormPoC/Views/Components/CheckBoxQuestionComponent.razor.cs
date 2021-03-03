using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class CheckBoxQuestionComponent : ComponentBase
    {
        [Parameter]
        public Question Question { get; set; }
        [Parameter]
        public List<OptionResponse> Responses { get; set; }

        public OptionResponse GetOptionResponse(Option option) =>
            this.Responses.Single(optionResponse => optionResponse.Option.Id == option.Id);
    }
}
