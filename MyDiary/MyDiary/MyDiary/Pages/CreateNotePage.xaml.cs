using System;
using System.Collections.Generic;
using System.Linq;
using MyDiary.Helpers;
using MyDiary.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDiary.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNotePage : ContentPage
    {
        public CreateNotePage()
        {
            InitializeComponent();
        }

        // Create note with photos and save them to SQLite DB
        private async void Save_OnClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionEditor.Text))
            {
                await DisplayAlert(ConstantHelper.Warning, ConstantHelper.NoteTextIsEmptyMessage, ConstantHelper.Ok);
                return;
            }

            if (!ViewModel.Photos.Any())
            {
                ViewModel.Photos.Add(new PhotoViewModel());
            }

            ViewModel.CreateOrUpdateNoteCommand.Execute(new NoteViewModel
            {
                Date = DateTime.Now,
                Description = DescriptionEditor.Text,
                Photos = new List<PhotoViewModel>(ViewModel.Photos)
            });
            await Navigation.PopAsync();
        }

        private void ViewModel_OnPhotoAdded(object sender, EventArgs e)
        {
            ImageGallery.Render();
        }
    }
}