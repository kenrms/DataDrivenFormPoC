using Microsoft.AspNetCore.Components;

namespace DataDrivenFormPoC.Views.Bases
{
    public partial class LabelBase : ComponentBase
    {
        [Parameter]
        public string Value { get; set; }

        public void SetValue(string value)
        {
            this.Value = value;
            InvokeAsync(StateHasChanged);
        }
    }
}
