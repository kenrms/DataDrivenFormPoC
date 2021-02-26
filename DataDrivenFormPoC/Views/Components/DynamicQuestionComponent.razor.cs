using DataDrivenFormPoC.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class DynamicQuestionComponent : ComponentBase
    {
        [Parameter]
        public Question Question { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            // get the component to view the product with
            Type componentType = GetViewComponentFromResponseType();
            // create an instance of this component
            builder.OpenComponent(0, componentType);
            // set the `Product` attribute of the component
            builder.AddAttribute(1, "Question", Question);
            // close
            builder.CloseComponent();
        }

        private Type GetViewComponentFromResponseType()
        {
            return Question.ResponseType switch
            {
                ResponseType.RawText => typeof(TextQuestionComponent),
                //, ResponseType.SingleChoice => expr
                //, ResponseType.MultipleChoice => expr
                //, ResponseType.DateTime => expr
                //, ResponseType.Number => expr
                //, ResponseType.Blob => expr
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
