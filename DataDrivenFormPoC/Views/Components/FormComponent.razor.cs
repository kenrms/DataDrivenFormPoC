using DataDrivenFormPoC.Models;
using DataDrivenFormPoC.Models.ContainerComponents;
using DataDrivenFormPoC.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class FormComponent : ComponentBase
    {
        [Inject]
        public IFormService FormService { get; set; }
        [Inject]
        public IValidationRuleService ValidationRuleService { get; set; }
        public ComponentState State { get; set; }
        public Form Form { get; set; }
        public FormResponse FormResponse { get; set; }
        public User CurrentUser { get; set; }
        public Dictionary<Guid, List<OptionResponse>> QuestionOptionResponsesMap;
        public Dictionary<Guid, List<ValidationRule>> QuestionValidationRulesMap;

        private EditContext FormEditContext { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.CurrentUser = await this.FormService.RetrieveDebugUserAsync();
            this.Form = await this.FormService.RetrieveDebugFormAsync();
            this.FormResponse = await this.FormService.RetrieveFormResponseForDebugFormAndUserAsync();
            InitializeQuestionOptionResponseMap();
            InitializeFormResponse();
            this.FormEditContext = new EditContext(this.FormResponse);
            this.State = ComponentState.Content;
        }

        private void InitializeFormResponse()
        {
            if (this.FormResponse == null)
            {
                this.FormResponse = new FormResponse
                {
                    FilledBy = this.CurrentUser,
                    Form = this.Form,
                    OptionResponses = GetOptionResponses(),
                };
            }
        }

        private void InitializeQuestionOptionResponseMap()
        {
            this.QuestionOptionResponsesMap = new Dictionary<Guid, List<OptionResponse>>();

            if (this.FormResponse != null)
            {
                this.QuestionOptionResponsesMap = this.FormResponse.OptionResponses
                    .GroupBy(optionResponse => optionResponse.Question.Id)
                    .ToDictionary(group => group.Key, group => group.ToList());
            }
            else
            {
                foreach (var question in this.Form.Questions)
                {
                    this.QuestionOptionResponsesMap[question.Id] = new List<OptionResponse>();

                    foreach (var option in question.Options)
                    {
                        var optionResponse = new OptionResponse
                        {
                            Option = option,
                            Question = question,
                        };

                        this.QuestionOptionResponsesMap[question.Id].Add(optionResponse);
                    }
                }
            }
        }

        public async void HandleSubmit()
        {
            bool formIsValid = true;

            // TODO validation bonanza; not a fan of this -- how to show errors in UI?
            foreach (var q in Form.Questions)
            {
                foreach (var qvr in q.QuestionValidationRules)
                {
                    var validator = this.ValidationRuleService
                        .GetResponseValidator(qvr.ValidationRule);

                    formIsValid = formIsValid &&
                        validator.Validate(QuestionOptionResponsesMap[q.Id]);
                }
            }

            // this shouldn't actually do anything yet
            this.FormEditContext.Validate();

            this.FormResponse.DateSubmitted = DateTimeOffset.Now;

            bool success = await this.FormService.SubmitFormResponseAsync(this.FormResponse);
        }

        private List<OptionResponse> GetOptionResponses() =>
            this.QuestionOptionResponsesMap
                .SelectMany(optionResponses => optionResponses.Value)
                .ToList();
    }
}
