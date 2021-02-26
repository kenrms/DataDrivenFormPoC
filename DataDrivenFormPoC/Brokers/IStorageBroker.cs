using DataDrivenFormPoC.Models;
using System.Linq;

namespace DataDrivenFormPoC.Brokers
{
    public partial interface IStorageBroker
    {
        public IQueryable<Form> SelectAllForms();
    }
}
