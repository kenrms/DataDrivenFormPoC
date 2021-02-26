using DataDrivenFormPoC.Models;
using System.Linq;

namespace DataDrivenFormPoC.Services
{
    public interface IFormService
    {
        IQueryable<Form> RetrieveAllForms();
    }
}
