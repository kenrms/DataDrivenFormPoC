using DataDrivenFormPoC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Brokers
{
    public interface IStorageBroker
    {
        ValueTask<IList<Form>> SelectAllFormsAsync();
        ValueTask<Form> SelectFormAsync(Guid formId);
        ValueTask<bool> AddOrUpdateFormResponseAsync(FormResponse formResponse);
        ValueTask<FormResponse> SelectFormResponseAsync(Guid userId, Guid formId);
        ValueTask<User> SelectUserAsync(Guid userId);
    }
}
