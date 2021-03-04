using DataDrivenFormPoC.Models;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Services.ValidationRules
{
    public class TextNotNullEmptyWhitespaceRule : ValidationRuleBase, IResponseValidator
    {
        public TextNotNullEmptyWhitespaceRule(QuestionValidationRule questionValidationRule)
            : base(questionValidationRule) { }

        public override bool Validate(List<OptionResponse> optionResponses)
        {
            foreach (var optionResponse in optionResponses)
            {
                if (string.IsNullOrWhiteSpace(optionResponse.TextValue))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
