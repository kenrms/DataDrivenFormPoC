using DataDrivenFormPoC.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataDrivenFormPoC.Data
{
    public class FormContext : DbContext, IStorageBroker
    {
        private readonly string ConnectionString =
            "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = DataDrivenFormPoCAppData";

        public DbSet<User> Users { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<FormResponse> FormResponses { get; set; }
        public DbSet<OptionResponse> OptionResponses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.ConnectionString);
        }
    }
}
