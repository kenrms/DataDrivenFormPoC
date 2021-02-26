using DataDrivenFormPoC.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataDrivenFormPoC.Data
{
    public class FormContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }
        public int MyProperty { get; set; }
    }
}
