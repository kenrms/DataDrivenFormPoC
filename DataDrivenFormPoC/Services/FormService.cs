using DataDrivenFormPoC.Brokers;
using DataDrivenFormPoC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Services
{
    public class FormService : IFormService
    {
        private readonly IStorageBroker storageBroker;

        public FormService(IStorageBroker storageBroker) => this.storageBroker = storageBroker;

        public async ValueTask<List<Form>> RetrieveAllFormsAsync()
        {
            // return storageBroker.SelectAllForms().ToList();

            return new List<Form>{
                new Form
                {
                    Id = Guid.NewGuid(),
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
                }
            };
        }
    }
}
