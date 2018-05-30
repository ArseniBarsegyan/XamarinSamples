using System;
using System.Linq;
using MyDiary.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDiary.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteDetailPage : ContentPage
    {
        public NoteDetailPage(NoteViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Title = $"{viewModel.Date:d}";
            DescriptionEditor.Text = viewModel.Description;
        }

        private async void Delete_OnClicked(object sender, EventArgs e)
        {
            if (BindingContext is NoteViewModel noteViewModel)
            {
                bool result = await DisplayAlert
                    ("Warning!", "Are you sure you want to delete this note?", "Ok", "Cancel");
                if (result)
                {
                    ViewModel.DeleteNoteCommand.Execute(noteViewModel);
                    await Navigation.PopAsync();
                }
            }
        }

        // When user focus Editor show "Confirm" at navigation bar
        private void DescriptionEditor_OnFocused(object sender, FocusEventArgs e)
        {
            var confirmToolbarItem = new ToolbarItem
            {
                Icon = "confirm.png"
            };
            confirmToolbarItem.Clicked += Confirm_OnClicked;
            ToolbarItems.Add(confirmToolbarItem);
        }

        // When user unfocus Editor hide "Confirm" at navigation bar
        private void DescriptionEditor_OnUnfocused(object sender, FocusEventArgs e)
        {
            var confirmToolbarItem = ToolbarItems.ElementAt(1);
            confirmToolbarItem.Clicked -= Confirm_OnClicked;
            ToolbarItems.Remove(confirmToolbarItem);
        }

        private void Confirm_OnClicked(object sender, EventArgs e)
        {
            if (BindingContext is NoteViewModel noteViewModel)
            {
                noteViewModel.Description = DescriptionEditor.Text;
                ViewModel.CreateOrUpdateNoteCommand.Execute(noteViewModel);
            }
        }
    }
}