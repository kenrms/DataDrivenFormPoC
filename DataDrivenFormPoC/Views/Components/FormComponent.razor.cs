using DataDrivenFormPoC.Models;
using DataDrivenFormPoC.Models.ContainerComponents;
using DataDrivenFormPoC.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class FormComponent : ComponentBase
    {
        [Inject]
        public IFormService FormService { get; set; }

        public ComponentState State { get; set; }
        public Form Form { get; set; }

        protected override void OnInitialized()
        {
            et this.Form = this.FormService.RetrieveAllForms().First();

            // TODO magically build RenderFragments based on Form data


            this.State = ComponentState.Content;
        }
    }
}
