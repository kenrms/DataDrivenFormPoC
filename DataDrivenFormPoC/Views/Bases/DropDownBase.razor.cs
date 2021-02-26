using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Bases
{
    public partial class DropDownBase<TEnum> : ComponentBase
    {
        [Parameter]
        public TEnum Value { get; set; }

        [Parameter]
        public EventCallback<TEnum> ValueChanged { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        public void SetValue(TEnum value) =>
            Value = value;

        private Task OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            Value = (TEnum)Enum.Parse(typeof(TEnum), changeEventArgs.Value.ToString());

            return ValueChanged.InvokeAsync(Value);
        }

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
