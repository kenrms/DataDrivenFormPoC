using DataDrivenFormPoC.Models;
using DataDrivenFormPoC.Services.ValidationRules;

namespace DataDrivenFormPoC.Services
{
    public interface IValidationRuleService
    {
        IResponseValidator GetResponsesValidator(QuestionValidationRule questionValidationRule);
    }
}
