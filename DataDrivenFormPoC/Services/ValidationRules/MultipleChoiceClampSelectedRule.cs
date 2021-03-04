using DataDrivenFormPoC.Models;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Services.ValidationRules
{
    public class MultipleChoiceClampSelectedRule : ValidationRuleBase, IResponseValidator
    {
        public MultipleChoiceClampSelectedRule(QuestionValidationRule questionValidationRule)
            : base(questionValidationRule) { }

        public override bool Validate(List<OptionResponse> optionResponses)
        {
            int checkedCount = 0;

            foreach (var optionResponse in optionResponses)
            {
                if (optionResponse.IsChecked)
                {
                    checkedCount++;
                }
            }

            return checkedCount >= QuestionValidationRule.MinValue &&
                checkedCount <= QuestionValidationRule.MaxValue;
        }
    }
}
