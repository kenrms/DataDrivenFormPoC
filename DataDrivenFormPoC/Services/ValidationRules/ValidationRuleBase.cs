using DataDrivenFormPoC.Models;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Services.ValidationRules
{
    public abstract class ValidationRuleBase : IResponseValidator
    {
        protected QuestionValidationRule QuestionValidationRule { get; set; }

        public ValidationRuleBase(QuestionValidationRule questionValidationRule)
        {
            QuestionValidationRule = questionValidationRule;
        }

        public virtual bool Validate(List<OptionResponse> optionResponses) => false;
    }
}