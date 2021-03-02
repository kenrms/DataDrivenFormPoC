using DataDrivenFormPoC.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Brokers
{
    public interface IStorageBroker
    {
        ValueTask<IQueryable<Form>> SelectAllForms();
        ValueTask<Form> SelectForm(Guid debugFormId);
    }
}
