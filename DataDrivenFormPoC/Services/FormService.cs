using DataDrivenFormPoC.Data;
using System;

namespace DataDrivenFormPoC.Services
{
    public class FormService : IFormService
    {
        private readonly FormContext FormContext;

        public FormService(FormContext formContext)
        {
            this.FormContext = formContext;
            this.FormContext.Database.EnsureCreated();
        }
    }
}
