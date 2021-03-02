﻿using DataDrivenFormPoC.Brokers;
using DataDrivenFormPoC.Models;
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

        public List<IOptionResponder> OptionResponders { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.Form = await this.FormService.RetrieveDebugFormAsync();
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
                DateSubmitted = DateTimeOffset.Now,
                Form = this.Form,
                Id = new Guid(),
                OptionResponses = optionResponses,
            };

            bool success = await this.FormService.SubmitFormResponse(formResponse);
            if (success)
            {
                var responses = await this.FormService.RetrieveOptionResponsesForDebugForm();
                // TODO populate UI with responses

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
