using System;
using MyDiary.ViewModels;
using Xamarin.Forms;

namespace MyDiary.Pages
{
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        private async void NotesList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NotesList.SelectedItem = null;
            if (e.SelectedItem is NoteViewModel viewModel)
            {
                await Navigation.PushAsync(new NoteDetailPage(viewModel));
            }
        }

        private async void Create_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateNotePage());
        }
    }
}
