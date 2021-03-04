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
        public Dictionary<Guid, List<string>> QuestionValidationMessagesMap { get; private set; }
        public string ValidationMessage { get; set; }

        private EditContext FormEditContext { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.CurrentUser = await this.FormService.RetrieveDebugUserAsync();
            this.Form = await this.FormService.RetrieveDebugFormAsync();
            this.FormResponse = await this.FormService.RetrieveFormResponseForDebugFormAndUserAsync();
            InitializeQuestionOptionResponseMap();
            InitializeQuestionValidationMessagesMap();
            InitializeFormResponse();
            this.FormEditContext = new EditContext(this.FormResponse);

            this.State = ComponentState.Content;
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

        private void InitializeQuestionValidationMessagesMap()
        {
            this.QuestionValidationMessagesMap = new Dictionary<Guid, List<string>>();

            foreach (var question in this.Form.Questions)
            {
                this.QuestionValidationMessagesMap[question.Id] = new List<string>();
            }
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

        public async void HandleSubmit()
        {
            this.ValidationMessage = string.Empty;
            bool isFormValid = ValidateForm();

            if (isFormValid)
            {
                this.FormResponse.DateSubmitted = DateTimeOffset.Now;
                await this.FormService.SubmitFormResponseAsync(this.FormResponse);
            }
            else
            {
                this.ValidationMessage = "Some validation error(s) occurred. Please fix them and try again.";
            }
        }

        private bool ValidateForm()
        {
            bool isFormValid = true;

            foreach (var question in Form.Questions)
            {
                this.QuestionValidationMessagesMap[question.Id].Clear();

                foreach (var questionValidationRule in question.QuestionValidationRules)
                {
                    var validationRule = this.ValidationRuleService
                        .GetResponsesValidator(questionValidationRule);

                    bool areResponsesValid = validationRule.Validate(QuestionOptionResponsesMap[question.Id]);

                    if (!areResponsesValid)
                    {
                        this.QuestionValidationMessagesMap[question.Id]
                            .Add(questionValidationRule.ValidationErrorMessage);
                    }

                    isFormValid = isFormValid && areResponsesValid;
                }
            }

            return isFormValid;
        }

        private List<OptionResponse> GetOptionResponses() =>
            this.QuestionOptionResponsesMap
                .SelectMany(optionResponses => optionResponses.Value)
                .ToList();
    }
}
