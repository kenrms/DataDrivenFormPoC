using DataDrivenFormPoC.Brokers;
using DataDrivenFormPoC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace DataDrivenFormPoC.Services
{
    public class FormService : IFormService
    {
        private readonly IStorageBroker storageBroker;

        private FormResponse debugFormResponse;

        public FormService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        private Form GenerateDebugFormFromFiller() => CreateFormFiller(100).Create();

        public async ValueTask<List<Form>> RetrieveAllFormsAsync()
        {
            var forms = await storageBroker.SelectAllForms();
            return forms.ToList();
        }

        public async ValueTask<Dictionary<Guid, List<OptionResponse>>> RetrieveOptionResponsesForDebugForm()
        {
            var result = new Dictionary<Guid, List<OptionResponse>>();
            var debugForm = await RetrieveDebugFormAsync();

            foreach (var question in debugForm.Questions)
            {
                var optionResponsesForQuestion =
                    debugFormResponse.OptionResponses
                        .Where(optionResponse =>
                            optionResponse.Question.Id == question.Id)
                        .ToList();

                result[question.Id] = optionResponsesForQuestion;
            }

            return result;
        }

        public async ValueTask<bool> SubmitFormResponse(FormResponse formResponse)
        {
            this.debugFormResponse = formResponse;

            return true;
        }

        public Filler<Form> CreateFormFiller(int numberOfQuestions)
        {
            var filler = new Filler<Form>();

            filler.Setup()
                .ListItemCount(numberOfQuestions);

            return filler;
        }

        public async ValueTask<Form> RetrieveDebugFormAsync() => 
            await storageBroker.SelectForm(StorageBroker.debugFormId);
    }
}
