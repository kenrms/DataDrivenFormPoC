using DataDrivenFormPoC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public StorageBroker(DbContextOptions<StorageBroker> options, IConfiguration configuration)
            : base(options)
        {
            this.Configuration = configuration;
            EnsureTestData();
            this.Database.Migrate();
        }

        private void EnsureTestData()
        {
            this.Database.EnsureCreated();

            if (!this.Forms.Any())
            {
                CreateSeedFormData();
            }
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public async ValueTask<IQueryable<Form>> SelectAllForms()
        {
            var form = this.Forms
                .Include(form => form.Questions.OrderBy(question => question.Order))
                .ThenInclude(question => question.Options.OrderBy(option => option.Order));

            return form;
        }

        public async ValueTask<Form> SelectForm(Guid debugFormId)
        {
            var form = this.Forms
                .Include(form => form.Questions.OrderBy(question => question.Order))
                .ThenInclude(question => question.Options.OrderBy(option => option.Order))
                .Single(form => form.Id == debugFormId);

            return form;
        }
    }
}
