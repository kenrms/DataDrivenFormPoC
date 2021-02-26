using DataDrivenFormPoC.Models;
using DataDrivenFormPoC.Models.ContainerComponents;
using DataDrivenFormPoC.Services;
using DataDrivenFormPoC.Views.Bases;
using Microsoft.AspNetCore.Components;

namespace DataDrivenFormPoC.Views.Components
{
    public partial class StudentRegistrationComponent : ComponentBase
    {
        [Inject]
        public IFormService FormService { get; set; }

        public ComponentState State { get; set; }
        public StudentRegistrationComponent Exception { get; set; }
        public Form Form { get; set; }
        public TextBoxBase StudentIdentityTextBox { get; set; }
        public TextBoxBase StudentFirstNameTextBox { get; set; }
        public TextBoxBase StudentMiddleNameTextBox { get; set; }
        public TextBoxBase StudentLastNameTextBox { get; set; }
        public DatePickerBase DateOfBirthPicker { get; set; }
        public ButtonBase SubmitButton { get; set; }
        public LabelBase StatusLabel { get; set; }

        protected override void OnInitialized()
        {
            this.State = ComponentState.Content;
        }
    }
}
