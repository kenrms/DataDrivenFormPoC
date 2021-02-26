using DataDrivenFormPoC.Brokers;
using DataDrivenFormPoC.Models;
using System.Linq;

namespace DataDrivenFormPoC.Services
{
    public class FormService : IFormService
    {
        private readonly IStorageBroker storageBroker;

        public FormService(IStorageBroker storageBroker) => this.storageBroker = storageBroker;

        public IQueryable<Form> RetrieveAllForms() => storageBroker.SelectAllForms();
    }
}
