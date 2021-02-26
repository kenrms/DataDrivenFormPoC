using DataDrivenFormPoC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Services
{
    public interface IFormService
    {
        ValueTask<List<Form>> RetrieveAllFormsAsync();
    }
}
