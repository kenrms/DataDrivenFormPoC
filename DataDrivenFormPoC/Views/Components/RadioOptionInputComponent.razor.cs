using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class RadioOptionInputComponent : ComponentBase
    {
        [Parameter]
        public Option Option { get; set; }

        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public EventCallback<ComponentBase> Callback { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await this.Callback.InvokeAsync(this);
        }
    }
}
