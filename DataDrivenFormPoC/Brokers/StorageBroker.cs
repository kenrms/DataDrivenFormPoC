using DataDrivenFormPoC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

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

        public StorageBroker(IConfiguration configuration)
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
            };

            this.Forms.Add(form);
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public IQueryable<Form> SelectAllForms() => this.Forms.AsQueryable();
    }
}
