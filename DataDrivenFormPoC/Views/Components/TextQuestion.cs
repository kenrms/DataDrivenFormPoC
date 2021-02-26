using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataDrivenFormPoC.Views.Bases;

namespace DataDrivenFormPoC.Views.Components
{
    public class TextQuestion : QuestionBase
    {
        public TextBoxBase TextBox { get; set; }

        /// <summary>
        /// Tell the consumer which component to use to view
        /// </summary>
        /// <returns></returns>
        public override Type GetViewComponent()
        {
            if (HasFlange)
                return typeof(Component1b);
            else
                return typeof(Component1);
        }

    }
}
