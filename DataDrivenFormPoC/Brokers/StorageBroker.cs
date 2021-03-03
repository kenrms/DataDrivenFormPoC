﻿using DataDrivenFormPoC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace DataDrivenFormPoC.Brokers
{
    public class StorageBroker : DbContext, IStorageBroker
    {
        private readonly IConfiguration Configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<FormResponse> FormResponses { get; set; }
        public DbSet<OptionResponse> OptionResponses { get; set; }

        public static readonly Guid debugFormId = new Guid("9da7e64f-6b44-4731-9dcb-4c398788879d");
        public static readonly Guid debugUserId = new Guid("4cb86f72-9523-4782-bc43-c272b8172a79");

        public StorageBroker(DbContextOptions<StorageBroker> options, IConfiguration configuration)
            : base(options)
        {
            this.Configuration = configuration;
            EnsureTestData();
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public async ValueTask<IList<Form>> SelectAllFormsAsync()
        {
            var forms = this.Forms
                .Include(form => form.Questions.OrderBy(question => question.Order))
                .ThenInclude(question => question.Options.OrderBy(option => option.Order));

            return await forms.ToListAsync();
        }

        public async ValueTask<Form> SelectFormAsync(Guid formId)
        {
            var form = this.Forms
                .Include(form => form.Questions.OrderBy(question => question.Order))
                    .ThenInclude(question => question.Options.OrderBy(option => option.Order))
                .Include(form => form.Questions.OrderBy(question => question.Order))
                    .ThenInclude(question => question.QuestionValidationRules)
                .SingleOrDefaultAsync(form => form.Id == formId);

            return await form;
        }

        public async ValueTask<bool> AddOrUpdateFormResponseAsync(FormResponse submittedFormResponse)
        {
            var existingFormResponse = await this.FormResponses
                .SingleOrDefaultAsync(formResponse =>
                    formResponse.Form.Id == submittedFormResponse.Form.Id &&
                    formResponse.FilledBy.Id == submittedFormResponse.FilledBy.Id);

            if (existingFormResponse == null)
            {
                await this.FormResponses.AddAsync(submittedFormResponse);
            }
            else
            {
                existingFormResponse.OptionResponses = submittedFormResponse.OptionResponses;
                existingFormResponse.DateSubmitted = submittedFormResponse.DateSubmitted;
                existingFormResponse.FilledBy = submittedFormResponse.FilledBy;
                existingFormResponse.Form = submittedFormResponse.Form;

                this.OptionResponses.UpdateRange(submittedFormResponse.OptionResponses);

                this.FormResponses.Update(existingFormResponse);
            }

            await this.SaveChangesAsync();

            return true;
        }

        public async ValueTask<FormResponse> SelectFormResponseAsync(Guid userId, Guid formId)
        {
            var formResponse = this.FormResponses
                .Include(formResponse => formResponse.OptionResponses)
                .SingleOrDefaultAsync(formResponse =>
                    formResponse.FilledBy.Id == userId && formResponse.Form.Id == formId);

            return await formResponse;
        }

        public async ValueTask<User> SelectUserAsync(Guid userId) =>
            await this.Users.SingleAsync(user => user.Id == userId);

        private Form GenerateDebugFormFromFiller() => CreateFormFiller(100).Create();

        public Filler<Form> CreateFormFiller(int numberOfQuestions)
        {
            var filler = new Filler<Form>();

            filler.Setup()
                .ListItemCount(numberOfQuestions);

            return filler;
        }

        private void EnsureTestData()
        {
            this.Database.EnsureCreated();

            if (!this.Forms.Any())
            {
                CreateSeedFormData();
            }

            if (!this.Users.Any())
            {
                CreateSeedUserData();
            }
        }

        private void CreateSeedUserData()
        {
            var user = new User
            {
                Id = debugUserId,
                Email = new EmailAddresses().GetValue()
            };

            this.Users.Add(user);
            this.SaveChanges();
        }

        private void CreateSeedFormData()
        {
            var form = new Form
            {
                Id = debugFormId,
                Questions = {
                            new Question
                            {
                                Id = Guid.NewGuid(),
                                IsRequired = false,
                                QuestionText = "Enter a name for this order:",
                                ResponseType = ResponseType.RawText,
                                Options = {
                                    new Option{ Id = Guid.NewGuid(), Order = 1 }
                                },
                                QuestionValidationRules = new List<QuestionValidationRule>
                                {
                                    new QuestionValidationRule
                                    {
                                        Id = Guid.NewGuid(),
                                        ValidationRule= ValidationRule.TextNotNullOrWhitespace
                                    },
                                },
                                Order = 1,
                            },
                            new Question
                            {
                                Id = Guid.NewGuid(),
                                IsRequired = false,
                                QuestionText = "Crust:",
                                ResponseType = ResponseType.SingleChoiceDropDown,
                                Options = {
                                    new Option {
                                        Id = Guid.NewGuid(),
                                        Value = "Hand-tossed",
                                        Order = 1
                                    },
                                    new Option{
                                        Id = Guid.NewGuid(),
                                        Value = "Thin-crust",
                                        Order = 2
                                    },
                                    new Option{
                                        Id = Guid.NewGuid(),
                                        Value = "Pan pizza",
                                        Order = 3
                                    },
                                },
                                Order = 2,
                            },
                            new Question
                            {
                                Id = Guid.NewGuid(),
                                IsRequired = false,
                                QuestionText = "Select your toppings: ",
                                ResponseType = ResponseType.MultipleChoice,
                                Options = {
                                    new Option {
                                        Id = Guid.NewGuid(),
                                        Value = "Pepperoni",
                                        Order = 1
                                    },
                                    new Option {
                                        Id = Guid.NewGuid(),
                                        Value = "Sausage",
                                        Order = 2
                                    },
                                    new Option {
                                        Id = Guid.NewGuid(),
                                        Value = "Bacon",
                                        Order = 3
                                    },
                                    new Option {
                                        Id = Guid.NewGuid(),
                                        Value = "Pineapple",
                                        Order = 4
                                    },
                                },
                                Order = 3,
                            },
                            new Question
                            {
                                Id = Guid.NewGuid(),
                                IsRequired = false,
                                QuestionText = "Extra cheese:",
                                ResponseType = ResponseType.SingleChoiceRadio,
                                Options = {
                                    new Option {
                                        Id = Guid.NewGuid(),
                                        Value = "Yes please!",
                                        Order = 1
                                    },
                                    new Option {
                                        Id = Guid.NewGuid(),
                                        Value = "No thanks",
                                        Order = 2
                                    },
                                },
                                Order = 4,
                            },
                            new Question
                            {
                                Id = Guid.NewGuid(),
                                IsRequired = false,
                                QuestionText = "When would you like your pizza?:",
                                ResponseType = ResponseType.Date,
                                Options = {
                                    new Option{ Id = Guid.NewGuid(), Order = 1 },
                                },
                                QuestionValidationRules = new List<QuestionValidationRule>
                                {
                                    new QuestionValidationRule
                                    {
                                        Id = Guid.NewGuid(),
                                        ValidationRule= ValidationRule.DateNotDefault
                                    },
                                },
                                Order = 5,
                            },
                    }
            };

            this.Forms.Add(form);
            this.SaveChanges();
        }
    }
}
