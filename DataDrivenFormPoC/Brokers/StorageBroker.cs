using DataDrivenFormPoC.Models;
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
                .SingleOrDefaultAsync(form => form.Id == formId);

            return await form;
        }

        public async ValueTask<bool> AddOrUpdateFormResponseAsync(FormResponse submittedFormResponse)
        {
            var existingFormResponse = await this.FormResponses
                .SingleOrDefaultAsync(formResponse => formResponse.Id == submittedFormResponse.Id);

            if (existingFormResponse == null)
            {
                await this.FormResponses.AddAsync(submittedFormResponse);
            }
            else
            {
                this.FormResponses.Update(submittedFormResponse);
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
                                QuestionText = "Question 1",
                                ResponseType = ResponseType.RawText,
                                Options = {
                                    new Option{ Id = Guid.NewGuid(), Order = 1 }
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
                                    new Option {
                                        Id = Guid.NewGuid(),
                                        Value = "A",
                                        Order = 1
                                    },
                                    new Option{
                                        Id = Guid.NewGuid(),
                                        Value = "B",
                                        Order = 2
                                    },
                                    new Option{
                                        Id = Guid.NewGuid(),
                                        Value = "C",
                                        Order = 3
                                    },
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
                                    new Option {
                                        Id = Guid.NewGuid(),
                                        Value = "Yes",
                                        Order = 1
                                    },
                                    new Option {
                                        Id = Guid.NewGuid(),
                                        Value = "No",
                                        Order = 2
                                    },
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
                                        Value = "Chicken",
                                        Order = 3
                                    },
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
                                    new Option{ Id = Guid.NewGuid(), Order = 1 },
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
