using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class FormSectionComponent : ComponentBase
    {
        [Parameter]
        public FormSection Section { get; set; }
        [Parameter]
        public int BaseIndex { get; set; }
        [Parameter]
        public Dictionary<Guid, List<OptionResponse>> ResponsesMap { get; set; }
        [Parameter]
        public Dictionary<Guid, List<string>> ValidationMessagesMap { get; set; }

    }
}
