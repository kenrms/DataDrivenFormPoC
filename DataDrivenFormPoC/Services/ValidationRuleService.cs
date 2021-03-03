using DataDrivenFormPoC.Models;
using DataDrivenFormPoC.Services.ValidationRules;
using System;

namespace DataDrivenFormPoC.Services
{
    public class ValidationRuleService : IValidationRuleService
    {
        public IResponseValidator GetResponseValidator(ValidationRule validationRule)
        {
            return validationRule switch
            {
                ValidationRule.TextNotNullOrWhitespace => new TextNotNullEmptyWhitespaceRule(),
                ValidationRule.DateNotDefault => new DateNotDefaultRule(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
