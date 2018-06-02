using System.Collections.Generic;

namespace Templates.ViewModels
{
    /// <summary>
    /// View model for Controls demo page.
    /// </summary>
    public class ControlsDemoViewModel : BaseViewModel
    {
        public ControlsDemoViewModel()
        {
            PickerItems = new List<string>
            {
                "Test item 1",
                "Test item 2",
                "Test item 3"
            };
        }

        public List<string> PickerItems { get; set; }
    }
}