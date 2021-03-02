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

        private readonly List<Form> debugForms;
        private Guid debugFormId = new Guid("9da7e64f-6b44-4731-9dcb-4c398788879d");
        private FormResponse debugFormResponse;

        public FormService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;

            this.debugForms = new List<Form> { GenerateDebugForm() };
        }

        private Form GenerateDebugForm()
        {
            Form form = new Form
            {
                Id = debugFormId,
                Questions = {
                            new Question
                            {
                                Id = Guid.NewGuid(),
                                IsRequired = false,
                                QuestionText = "Question 1",
                                ResponseType = ResponseType.RawText,
                                Options = {
                                    new Option{ Id = Guid.NewGuid() }
                                },
                                Order = 1,
                            },
                            new Question
                            {
                                Id = Guid.NewGuid(),
                                IsRequired = false,
                                QuestionText = "Question 2",
                                ResponseType = ResponseType.SingleChoiceDropDown,
                                Options = {
                                    new Option{ Id = Guid.NewGuid(), Value = "A"},
                                    new Option{ Id = Guid.NewGuid(), Value = "B"},
                                    new Option{ Id = Guid.NewGuid(), Value = "C"},
                                    new Option{ Id = Guid.NewGuid(), Value = "D"},
                                    new Option{ Id = Guid.NewGuid(), Value = "E"},
                                },
                                Order = 2,
                            },
                            new Question
                            {
                                Id = Guid.NewGuid(),
                                IsRequired = false,
                                QuestionText = "Question 3",
                                ResponseType = ResponseType.SingleChoiceRadio,
                                Options = {
                                    new Option{ Id = Guid.NewGuid(), Value = "Yes"},
                                    new Option{ Id = Guid.NewGuid(), Value = "No"},
                                    new Option{ Id = Guid.NewGuid(), Value = "Maybe"},
                                },
                                Order = 3,
                            },
                            new Question
                            {
                                Id = Guid.NewGuid(),
                                IsRequired = false,
                                QuestionText = "Question 4",
                                ResponseType = ResponseType.MultipleChoice,
                                Options = {
                                    new Option{ Id = Guid.NewGuid(), Value = "Pepperoni"},
                                    new Option{ Id = Guid.NewGuid(), Value = "Sausage"},
                                    new Option{ Id = Guid.NewGuid(), Value = "Chicken"},
                                    new Option{ Id = Guid.NewGuid(), Value = "Mushrooms"},
                                },
                                Order = 4,
                            },
                            new Question
                            {
                                Id = Guid.NewGuid(),
                                IsRequired = false,
                                QuestionText = "Question 5",
                                ResponseType = ResponseType.Date,
                                Options = {
                                    new Option{ Id = Guid.NewGuid() },
                                },
                                Order = 5,
                            },
                    }
            };

            return form;
        }

        private Form GenerateDebugFormFromFiller() => CreateFormFiller(100).Create();

        public async ValueTask<List<Form>> RetrieveAllFormsAsync()
        {
            // return storageBroker.SelectAllForms().ToList();

            return debugForms;
        }

        public async ValueTask<Dictionary<Guid, List<OptionResponse>>> RetrieveOptionResponsesForDebugForm()
        {
            var result = new Dictionary<Guid, List<OptionResponse>>();

            foreach (var question in debugForms.First().Questions)
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
    }
}
