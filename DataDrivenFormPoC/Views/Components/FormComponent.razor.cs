using DataDrivenFormPoC.Models;
using DataDrivenFormPoC.Models.ContainerComponents;
using DataDrivenFormPoC.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class FormComponent : ComponentBase
    {
        [Inject]
        public IFormService FormService { get; set; }

        [Parameter]
        public FormComponent FormComponentRef { get; set; }

        public ComponentState State { get; set; }
        public Form Form { get; set; }

        public List<IOptionResponder> OptionResponders { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.Form = (await FormService.RetrieveAllFormsAsync()).First();
            this.OptionResponders = new List<IOptionResponder>();
            this.State = ComponentState.Content;
        }

        public ResponseType GetType(Question question) =>
            question.ResponseType;

        public void HandleSubmit()
        {
            List<OptionResponse> optionResponses = GetOptionResponses();
            // TODO send to API
        }

        private List<OptionResponse> GetOptionResponses()
        {
            var result = new List<OptionResponse>();

            foreach (var optionResponder in this.OptionResponders)
            {
                var optionResponses = optionResponder.GetOptionResponses();
                result.AddRange(optionResponses);
            }

            return result;
        }

        public void AddQuestionComponentToOptionResponders(IOptionResponder optionResponder)
        {
            this.OptionResponders.Add(optionResponder);
        }
    }
}
