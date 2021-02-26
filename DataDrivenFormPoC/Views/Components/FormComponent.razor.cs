using DataDrivenFormPoC.Models;
using DataDrivenFormPoC.Models.ContainerComponents;
using DataDrivenFormPoC.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class FormComponent : ComponentBase
    {
        [Inject]
        public IFormService FormService { get; set; }

        public ComponentState State { get; set; }
        public Form Form { get; set; }

        
        protected async override Task OnInitializedAsync()
        {
            this.Form = (await FormService.RetrieveAllFormsAsync()).First();

            this.State = ComponentState.Content;
        }

        public ResponseType GetType(Question question) =>
            question.ResponseType;
    }
}
