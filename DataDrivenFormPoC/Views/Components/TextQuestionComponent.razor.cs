﻿using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class TextQuestionComponent : ComponentBase, IOptionResponder
    {
        [Parameter]
        public Question Question { get; set; }

        [Parameter]
        public EventCallback<IOptionResponder> Callback { get; set; }

        [Parameter]
        public List<OptionResponse> Responses { get; set; }

        protected Guid GetOptionId() => Question.Options.First().Id;

        protected async override Task OnInitializedAsync() =>
            await this.Callback.InvokeAsync(this);

        public OptionResponse GetOptionResponse() =>
            Responses.Single();

        public IList<OptionResponse> GetOptionResponses() =>
            this.Responses;
    }
}
