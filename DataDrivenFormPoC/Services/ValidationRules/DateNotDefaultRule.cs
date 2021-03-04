using DataDrivenFormPoC.Models;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Services.ValidationRules
{
    public class DateNotDefaultRule : ValidationRuleBase, IResponseValidator
    {
        public DateNotDefaultRule(QuestionValidationRule questionValidationRule)
            : base(questionValidationRule) { }

        public override bool Validate(List<OptionResponse> optionResponses)
        {
            foreach (var optionResponse in optionResponses)
            {
                if (optionResponse.DateTimeValue == default)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
