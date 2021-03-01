using DataDrivenFormPoC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Services
{
    public interface IFormService
    {
        ValueTask<List<Form>> RetrieveAllFormsAsync();
        ValueTask<Dictionary<Guid, OptionResponse>> RetrieveOptionResponsesForForm(Guid formId);
        void SubmitFormResponse(FormResponse formResponse);
    }
}
