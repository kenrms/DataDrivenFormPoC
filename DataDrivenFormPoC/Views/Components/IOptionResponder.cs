using DataDrivenFormPoC.Models;
using System.Collections.Generic;

namespace DataDrivenFormPoC.Views.Components
{
    public interface IOptionResponder
    {
        IList<OptionResponse> GetOptionResponses();
    }
}