using System;
using MyDiary.ViewModels;
using Xamarin.Forms;

namespace MyDiary.Pages
{
    /// <inheritdoc />
    /// <summary>
    /// List of notes with search bar at the top.
    /// </summary>
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
            if (e.SelectedItem is NoteViewModel viewModel)
            {
                await Navigation.PushAsync(new NoteDetailPage(viewModel));
            }
            NotesList.SelectedItem = null;
        }

        private async void Create_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateNotePage());
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.SearchCommand.Execute(SearchBar.Text);
        }
    }
}
