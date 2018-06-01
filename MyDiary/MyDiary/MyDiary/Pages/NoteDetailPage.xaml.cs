using System;
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