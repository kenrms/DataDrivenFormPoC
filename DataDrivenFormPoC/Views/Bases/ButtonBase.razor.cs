using Microsoft.AspNetCore.Components;
using System;

namespace DataDrivenFormPoC.Views.Bases
{
    public partial class ButtonBase : ComponentBase
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public Action OnClick { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        public void Click() => OnClick.Invoke();

        public void Disable()
        {
            IsDisabled = true;
            InvokeAsync(StateHasChanged);
        }

        public void Enable()
        {
            IsDisabled = false;
            InvokeAsync(StateHasChanged);
        }
    }
}
