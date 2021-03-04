using DataDrivenFormPoC.Models;
using DataDrivenFormPoC.Services.ValidationRules;
using System;

namespace DataDrivenFormPoC.Services
{
    public class ValidationRuleService : IValidationRuleService
    {
        public IResponseValidator GetResponsesValidator(QuestionValidationRule questionValidationRule)
        {
            return questionValidationRule.ValidationRule switch
            {
                ValidationRule.TextNotNullOrWhitespace =>
                    new TextNotNullEmptyWhitespaceRule(questionValidationRule),
                ValidationRule.DateNotDefault =>
                    new DateNotDefaultRule(questionValidationRule),
                ValidationRule.MultipleChoiceClampSelected =>
                    new MultipleChoiceClampSelectedRule(questionValidationRule),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
