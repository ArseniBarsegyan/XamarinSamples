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

        private async void Confirm_OnClicked(object sender, EventArgs e)
        {
            if (BindingContext is NoteViewModel noteViewModel)
            {
                noteViewModel.Description = DescriptionEditor.Text;
                ViewModel.CreateOrUpdateNoteCommand.Execute(noteViewModel);
                await Navigation.PopAsync();
            }
        }

        private async void Delete_OnClicked(object sender, EventArgs e)
        {
            if (BindingContext is NoteViewModel noteViewModel)
            {
                ViewModel.DeleteNoteCommand.Execute(noteViewModel);
                await Navigation.PopAsync();
            }
        }
    }
}