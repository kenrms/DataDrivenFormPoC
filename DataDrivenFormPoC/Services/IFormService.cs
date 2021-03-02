using DataDrivenFormPoC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Services
{
    public interface IFormService
    {
        ValueTask<List<Form>> RetrieveAllFormsAsync();
        ValueTask<bool> SubmitFormResponseAsync(FormResponse formResponse);
        ValueTask<bool> SubmitDebugFormResponseAsync(FormResponse formResponse);
        ValueTask<Form> RetrieveDebugFormAsync();
        ValueTask<FormResponse> RetrieveFormResponseForDebugFormAndUserAsync();
        ValueTask<User> RetrieveDebugUserAsync();
    }
}
