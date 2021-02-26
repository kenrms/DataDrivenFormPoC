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
                var testForm = new Form
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
                                new Option()
                            },
                            Order = 1,
                        },
                        new Question
                        {
                            Id = Guid.NewGuid(),
                            IsRequired = false,
                            QuestionText = "Question 2",
                            ResponseType = ResponseType.SingleChoice,
                            Options = {
                                new Option{ Value = "A"},
                                new Option{ Value = "B"},
                                new Option{ Value = "C"},
                            },
                            Order = 2,
                        },
                        new Question
                        {
                            Id = Guid.NewGuid(),
                            IsRequired = false,
                            QuestionText = "Question 3",
                            ResponseType = ResponseType.SingleChoice,
                            Options = {
                                new Option{ Value = "Yes"},
                                new Option{ Value = "No"},
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
                                new Option{ Value = "Pepperoni"},
                                new Option{ Value = "Sausage"},
                                new Option{ Value = "Chicken"},
                                new Option{ Value = "Mushrooms"},
                            },
                            Order = 4,
                        },
                    }
                };

                this.Forms.Add(testForm);
                this.SaveChanges();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public IQueryable<Form> SelectAllForms() => this.Forms.AsQueryable();
    }
}
