using DataDrivenFormPoC.Models;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Services.ValidationRules
{
    public interface IResponseValidator
    {
        bool Validate(List<OptionResponse> optionResponses);
    }
}