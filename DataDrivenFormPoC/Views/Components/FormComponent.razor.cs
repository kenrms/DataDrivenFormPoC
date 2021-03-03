﻿using DataDrivenFormPoC.Models;
using DataDrivenFormPoC.Models.ContainerComponents;
using DataDrivenFormPoC.Services;
using Microsoft.AspNetCore.Components;
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
        public ComponentState State { get; set; }
        public Form Form { get; set; }
        public FormResponse FormResponse { get; set; }
        public User CurrentUser { get; set; }
        public Dictionary<Guid, List<OptionResponse>> QuestionOptionResponsesMap;

        protected async override Task OnInitializedAsync()
        {
            this.CurrentUser = await this.FormService.RetrieveDebugUserAsync();
            this.Form = await this.FormService.RetrieveDebugFormAsync();
            this.FormResponse = await this.FormService.RetrieveFormResponseForDebugFormAndUserAsync();
            InitializeQuestionOptionResponseMap();
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

        public ResponseType GetQuestionResponseType(Question question) =>
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

        private List<OptionResponse> GetOptionResponses() =>
            this.QuestionOptionResponsesMap
                .SelectMany(optionResponses => optionResponses.Value)
                .ToList();
    }
}
