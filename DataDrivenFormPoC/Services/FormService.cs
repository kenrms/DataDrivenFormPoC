using DataDrivenFormPoC.Brokers;
using DataDrivenFormPoC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDrivenFormPoC.Services
{
    public class FormService : IFormService
    {
        private readonly IStorageBroker storageBroker;

        public FormService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<List<Form>> RetrieveAllFormsAsync()
        {
            var forms = await storageBroker.SelectAllFormsAsync();
            return forms.ToList();
        }

        public async ValueTask<bool> SubmitFormResponseAsync(FormResponse formResponse) =>
            await this.storageBroker.AddOrUpdateFormResponseAsync(formResponse);

        public async ValueTask<bool> SubmitDebugFormResponseAsync(FormResponse formResponse) =>
            await this.storageBroker.AddOrUpdateFormResponseAsync(formResponse);

        public async ValueTask<Form> RetrieveDebugFormAsync() =>
            await storageBroker.SelectFormAsync(StorageBroker.debugFormId);

        public async ValueTask<FormResponse> RetrieveFormResponseForDebugFormAndUserAsync() =>
            await this.storageBroker
                .SelectFormResponseAsync(
                    StorageBroker.debugUserId,
                    StorageBroker.debugFormId);

        public ValueTask<User> RetrieveDebugUserAsync() =>
            this.storageBroker.SelectUserAsync(StorageBroker.debugUserId);
    }
}
