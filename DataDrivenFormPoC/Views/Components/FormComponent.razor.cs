using DataDrivenFormPoC.Models;
using DataDrivenFormPoC.Models.ContainerComponents;
using DataDrivenFormPoC.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class FormComponent : ComponentBase
    {
        [Inject]
        public IFormService FormService { get; set; }

        public ComponentState State { get; set; }
        public Form Form { get; set; }
        public FormResponse FormResponse { get; set; }
        public User CurrentUser { get; set; }

        public List<IOptionResponder> OptionResponders { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.CurrentUser = await this.FormService.RetrieveDebugUserAsync();
            this.Form = await this.FormService.RetrieveDebugFormAsync();
            this.FormResponse = await this.FormService.RetrieveFormResponseForDebugFormAndUserAsync();

            this.OptionResponders = new List<IOptionResponder>();
            this.State = ComponentState.Content;
        }

        public ResponseType GetType(Question question) =>
            question.ResponseType;

        public async void HandleSubmit()
        {
            List<OptionResponse> optionResponses = GetOptionResponses();

            var formResponse = new FormResponse
            {
                FilledBy = this.CurrentUser,
                DateSubmitted = DateTimeOffset.Now,
                Form = this.Form,
                OptionResponses = optionResponses,
            };

            bool success = await this.FormService.SubmitFormResponseAsync(formResponse);
            if (success)
            {
                this.FormResponse = await this.FormService
                    .RetrieveFormResponseForDebugFormAndUserAsync();
            }
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
